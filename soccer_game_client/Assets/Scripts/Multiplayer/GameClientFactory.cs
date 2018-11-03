using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClientFactory
{
    public static GameClient Create()
    {
        return new TextGameClient("../sessions/");
    }
}
