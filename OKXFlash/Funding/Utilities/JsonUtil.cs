/*
 * Copyright (C) 2024 The Hong-Jin Investment Company.
 * This file is part of the OKX Trading Server.
 * File created at 2024-12-05
 */
namespace HongJinInvestment.OKX.Server;

using Newtonsoft.Json.Linq;

public static class JsonUtil
{
    public static JObject TryPraseOkxResultJson(string json)
    {
        JObject jsonRoot = JObject.Parse(json);
        string code = jsonRoot["code"].ToString();
        if (code != "0")
        {
            string msg = jsonRoot["msg"].ToString();
            LogManager.Instance.LogError($"Invalid response failed, reason: {msg}");
            return null;
        }

        return jsonRoot;
    }
}
