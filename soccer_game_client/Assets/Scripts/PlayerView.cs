using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    private Match m_match;
    private PlayerId m_playerId;
    private Animator m_animator;
    public PlayerAnimationType AnimationType { get; private set; }

    private void Awake()
    {
        m_animator = transform.Find("Model").GetComponent<Animator>();
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

    public void SetAnim(PlayerAnimationType animType)
    {
        if (AnimationType == animType)
            return;

        AnimationType = animType;

        string clipName = null;

        switch (animType)
        {
            case PlayerAnimationType.Idle:
                clipName = "Idle";
                break;

            case PlayerAnimationType.Slide:
                clipName = "Slide";
                break;

        }

        if (clipName != null)
            m_animator.SetTrigger(clipName);
    }
}
