using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : IBall
{
    private BallView m_ballView;
    private IPlayer m_player;

    public Ball(BallView ballView)
    {
        m_ballView = ballView;
    }

    public IPlayer GetPlayer()
    {
        return m_player;
    }

    public void SetPlayer(IPlayer player)
    {
        m_player = player;
    }

    public void ClearPlayer()
    {
        m_player = null;
    }

    public void EnablePhysics(bool enable)
    {
        m_ballView.EnablePhysics(enable);
    }

    public Vector3 GetPosition()
    {
        return m_ballView.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        m_ballView.SetPosition(position);
    }

    public void SetVelocity(Vector3 velocity)
    {
        m_ballView.SetVelocity(velocity);
    }
}
