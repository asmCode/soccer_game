using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : PlayerState
{
    private static PlayerStateIdle m_instance;

    private PlayerStateIdle() { }

    public static PlayerStateIdle Get()
    {
        if (m_instance == null)
            m_instance = new PlayerStateIdle();

        return m_instance;
    }

    public override void Enter(IPlayer player)
    {
        player.PlayAnimation(PlayerAnimationType.Idle);
        (player as Player).PhysicsObject.Velocity = Vector3.zero;
    }

    public override void Run(IPlayer player, PlayerDirection direction, float deltaTime)
    {
        player.State = PlayerStateRun.Get();
        player.State.Enter(player);
        player.State.Run(player, direction, deltaTime);
    }
}
