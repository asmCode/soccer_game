using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateSlide : PlayerState
{
    private static PlayerStateSlide m_instance;

    private PlayerStateSlide() { }

    public static PlayerStateSlide Get()
    {
        if (m_instance == null)
            m_instance = new PlayerStateSlide();

        return m_instance;
    }

    public override void Enter(IPlayer player)
    {
        player.PlayAnimation(PlayerAnimationType.Slide);
        player.SlideTime = 0.0f;
        player.SlideBasePos = player.GetPosition();

        var player2 = player as Player;
        player2.PhysicsObject.Velocity = player2.GetDirectionVector() * GameSettings.SlideSpeed;
    }

    public override void Update(Player player, float deltaTime)
    {
        if (player.PhysicsObject.Velocity.sqrMagnitude == 0.0f)
            player.SetIdle();
    }
}
