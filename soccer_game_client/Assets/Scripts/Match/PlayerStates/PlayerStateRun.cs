using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateRun : PlayerState
{
    private static PlayerStateRun m_instance;

    private PlayerStateRun() { }

    public static PlayerStateRun Get()
    {
        if (m_instance == null)
            m_instance = new PlayerStateRun();

        return m_instance;
    }

    public override void Enter(IPlayer player)
    {
        player.PlayAnimation(PlayerAnimationType.Run);
    }

    public override void Run(IPlayer player, PlayerDirection direction, float deltaTime)
    {
        player.SetDirection(direction);
        player.OffsetPosition(PlayerDirectionVector.GetVector(direction) * deltaTime * GameSettings.PlayerSpeed);
    }
}
