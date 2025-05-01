/*
 * Copyright (C) 2024 The Hong-Jin Investment Company.
 * This file is part of the OKX Trading Server.
 * File created at 2024-12-11
 */
namespace HongJinInvestment.OKX.Server.Internal
{
    public class HttpRequestInternal
    {
        private readonly HttpClient m_Client = new HttpClient();

        public Task<HttpResponseMessage> GetAsync(string url, Action<string>? callback = null)
        {
            return Task.Run(async () =>
            {
                HttpResponseMessage response = await m_Client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    string rawJsonString = await response.Content.ReadAsStringAsync();
                    callback?.Invoke(rawJsonString);
                }

                HttpRequest.Recycle(this);
                return response;
            });
        }

        public void GetSync(string url, Action<string>? callback = null)
        {
            HttpResponseMessage response = m_Client.GetAsync(url).Result;
            if (response.IsSuccessStatusCode)
            {
                string rawJsonString = response.Content.ReadAsStringAsync().Result;
                callback?.Invoke(rawJsonString);
            }
        }

        public Task<HttpResponseMessage> PostAsync(string url, StringContent content, Action<string>? callback = null)
        {
            return (Task<HttpResponseMessage>)Task.Run(async () =>
            {
                HttpResponseMessage response = await m_Client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    string rawJsonString = await response.Content.ReadAsStringAsync();
                    callback?.Invoke(rawJsonString);
                }

                HttpRequest.Recycle(this);
            });
        }

        public void PostSync(string url, StringContent content, Action<string>? callback = null)
        {
            HttpResponseMessage response = m_Client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                string rawJsonString = response.Content.ReadAsStringAsync().Result;
                callback?.Invoke(rawJsonString);
            }
        }
    }
}

namespace HongJinInvestment.OKX.Server
{
    using System;
    using HttpRequestInternal = Internal.HttpRequestInternal;

    public static class HttpRequest
    {
        private static readonly Stack<HttpRequestInternal> ms_HttpRequestInternals = new Stack<HttpRequestInternal>();

        private static readonly HttpRequestInternal m_HttpRequestSync = new HttpRequestInternal();

        public static void Get(string url, Action<string> callback)
        {
            lock (ms_HttpRequestInternals)
            {
                if (ms_HttpRequestInternals.TryPop(out var httpRequest))
                {
                    httpRequest.GetSync(url, callback);
                }
                else
                {
                    httpRequest = new HttpRequestInternal();
                    httpRequest.GetSync(url, callback);
                }
            }   
        }

        public static void GetSync(string url, Action<string>? callback = null)
        {
            m_HttpRequestSync.GetSync(url, callback);
        }

        public static HttpRequestInternal GetTemp()
        {
            lock (ms_HttpRequestInternals)
            {
                if (ms_HttpRequestInternals.TryPop(out var httpRequest))
                {
                    return httpRequest;
                }
                else
                {
                   return new HttpRequestInternal();
                }
            }
        }

        public static void Recycle(HttpRequestInternal httpRequest)
        {
            lock (ms_HttpRequestInternals)
            {
                ms_HttpRequestInternals.Push(httpRequest);
            }
        }
    }
}