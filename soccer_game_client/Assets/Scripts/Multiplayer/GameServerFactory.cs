using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameServerFactory
{
    public static GameServer Create()
    {
        return new TextGameServer("../sessions/");
    }
}
