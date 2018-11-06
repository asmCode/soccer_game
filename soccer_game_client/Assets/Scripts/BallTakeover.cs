using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTakeover : MonoBehaviour
{
    private PlayerView m_player;

    private void Awake()
    {
        m_player = transform.parent.GetComponent<PlayerView>();
    }

    private void OnTriggerEnter(Collider other)
    {
        m_player.TriggerBallCollision();
    }
}
