/*
 * Copyright (C) 2024 The Hong-Jin Investment Company.
 * This file is part of the OKX Trading Server.
 * File created at 2024-12-05
 */
namespace HongJinInvestment.OKX.Server;

using System.Net.Sockets;
/*
 * 客户端实体，每一个
 */
public class ClientEntity
{
    // 客户端连接的ID
    public long ID { get; private set; }
    
    // 客户端连接时间
    public long ConnectedTimestamp { get; private set; }
    
    // 上次收到客户端心跳包时间
    public long LastHeartBeatTimestamp { get; private set; }
    
    // 客户端Socket对象
    private Socket m_Socket;
}