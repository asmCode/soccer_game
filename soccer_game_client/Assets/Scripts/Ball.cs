using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Create an IBall interface and do the same steps as for the player.

public class Ball
{
    public IPlayer Player { get; private set; }

    public void SetPlayer(IPlayer player)
    {
        Player = player;
    }

    public void ClearPlayer()
    {
        SetPlayer(null);
    }
}
