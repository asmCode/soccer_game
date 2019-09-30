using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    void Enter(IPlayer player);
    void Leave(IPlayer player);
    void Update(IPlayer player, float deltaTime);
}
