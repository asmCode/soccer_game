using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServerFactory
{
    public static GameServer Create()
    {
        return CreateUdpGameServer();
    }

    private static GameServer CreateTextServer()
    {
        return new TextGameServer("../sessions/");
    }

    private static GameServer CreateUdpGameServer()
    {
        return new UdpGameServer(GameSettings.ServerDefaultPort);
    }
}
