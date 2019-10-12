using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// It might be better to keep states in the match and player will holds only enum of the state, which is also index of the state in the match array of states.

public class PlayerState
{
    public virtual void Enter(IPlayer player) { }
    public virtual void Leave(IPlayer player) { }
    public virtual void Update(Player player, float deltaTime) { }
    public virtual void Run(IPlayer player, PlayerDirection direction, float deltaTime) { }
    public virtual void Slide(Player player) { }
    public virtual void StopRunning(Player player) { }
    public virtual void BallCollision(Player player, Match match) { }
}
