/*
 * Copyright (C) 2024 The Hong-Jin Investment Company.
 * This file is part of the OKX Trading Server.
 * File created at 2024-12-05
 */

using SuperSocket.SocketBase;
using SuperWebSocket;

namespace HongJinInvestment.OKX.Server;

using System.Net;
using System.Net.WebSockets;
using System.Net.Sockets;

public class GetRequest
{
    public GetRequest(string url) 
    {
        
    }
}

public class OkxServer:BehaviourSingleton<OkxServer>
{
    // 用于监听客户端与发送消息的WebSocketServer对象
    private readonly WebSocketServer m_Socket;

    public OkxServer()
    {
        m_Socket = new WebSocketServer();
        m_Socket.NewSessionConnected += this.OnNewSessionConnected;
        m_Socket.NewMessageReceived += this.OnNewMessageReceived;
        m_Socket.SessionClosed += this.OnSessionClosed;
    }

    private void OnNewSessionConnected(WebSocketSession session)
    { 
        
    }

    private void OnNewMessageReceived(WebSocketSession session, string value)
    {
        
    }

    private void OnSessionClosed(WebSocketSession session, CloseReason value)
    {
        
    }
    
    public override void OnStart()
    {
        if (!m_Socket.Setup("127.0.0.1", NetworkConfig.Port) || !m_Socket.Start())
        {
            LogManager.Instance.LogError("设置服务监听失败！");
        }
    }

    public override void OnUpdate(float deltaTime)
    {
       
    }

    public override void OnDestroy()
    {
        
    }
}