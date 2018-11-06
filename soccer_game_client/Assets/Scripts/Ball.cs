using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball
{
    public Player Player { get; private set; }

    public void SetPlayer(Player player)
    {
        Player = player;
    }

    public void ClearPlayer()
    {
        SetPlayer(null);
    }
}
