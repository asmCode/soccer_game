using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Match m_match;
    private PlayerId m_playerId;
    private Animator m_animator;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public PlayerId PlayerId
    {
        get { return m_playerId; }
    }

    public void Init(Match match, byte teamIndex, byte playerIndex)
    {
        m_match = match;
        m_playerId.Team = teamIndex;
        m_playerId.Index = playerIndex;
    }

    public ssg.CapsuleCollider GetBallTakeoverCollider()
    {
        var collider = transform.Find("BallTakeover").gameObject.GetComponent<CapsuleCollider>();
        var capsuleCollider = new ssg.CapsuleCollider();
        capsuleCollider.LocalCenter = collider.center;
        capsuleCollider.Radius = collider.radius;
        return capsuleCollider;
    }

    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }

    public Vector3 Direction
    {
        set { transform.forward = value; }
    }

    public void TriggerBallCollision()
    {
        m_match.NotifyPlayerBallCollision(PlayerId);
    }

    public void SetIdleAnim()
    {
        m_animator.SetTrigger("Idle");
    }

    public void SetSlideAnim()
    {
        m_animator.SetTrigger("Slide");
    }
}
