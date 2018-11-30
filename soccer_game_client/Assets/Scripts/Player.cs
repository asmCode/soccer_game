using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : IPlayer
{
    private PlayerView m_playerView;
    private PlayerDirection m_direction;

    public byte Team { get; set; }
    public byte Index { get; set; }

    public Player(PlayerView playerView, byte team, byte index, PlayerDirection playerDirection)
    {
        m_playerView = playerView;
        Team = team;
        Index = index;
        m_direction = playerDirection;

        SetDirection(playerDirection);
    }

    public Vector3 GetPosition()
    {
        return m_playerView.transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        m_playerView.transform.position = position;
    }

    public void SetDirection(PlayerDirection direction)
    {
        m_direction = direction;
        m_playerView.transform.forward = GetDirectionVector();
    }

    public PlayerDirection GetDirection()
    {
        return m_direction;
    }

    public Vector3 GetDirectionVector()
    {
        return PlayerDirectionVector.GetVector(m_direction);
    }
}
