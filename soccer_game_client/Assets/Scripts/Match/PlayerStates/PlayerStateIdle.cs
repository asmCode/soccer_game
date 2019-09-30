using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateIdle : IPlayerState
{
    private static PlayerStateIdle m_instance;

    private PlayerStateIdle() { }

    public static PlayerStateIdle Get()
    {
        if (m_instance == null)
            m_instance = new PlayerStateIdle();

        return m_instance;
    }

    public void Enter(IPlayer player)
    {
        player.PlayAnimation(PlayerAnimationType.Idle);
    }

    public void Leave(IPlayer player)
    {
    }

    public void Update(IPlayer player, float deltaTime)
    {
    }
}
