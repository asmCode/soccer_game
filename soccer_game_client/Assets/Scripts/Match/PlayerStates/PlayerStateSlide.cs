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

    public override void Update(IPlayer player, float deltaTime)
    {
        player.SlideTime = Mathf.Min(player.SlideTime + deltaTime, GameSettings.SlideDuration);

        // Vector3 newPos = player.SlideBasePos + player.GetDirectionVector() * SlideCurve.Get().m_curve.Evaluate(player.SlideTime / GameSettings.SlideDuration) * GameSettings.SlideDistance;
        // Vector3 newPos = player.SlideBasePos + player.GetDirectionVector() * SlideCurve.Get().m_curve.Evaluate(1.0f) * GameSettings.SlideDistance;
        // player.SetPosition(newPos);

        

        if (player.SlideTime >= GameSettings.SlideDuration)
            player.SetIdle();
    }
}
