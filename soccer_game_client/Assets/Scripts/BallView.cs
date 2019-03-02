using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallView : MonoBehaviour
{
    private Rigidbody m_rigibody;

    public void EnablePhysics(bool enable)
    {
        m_rigibody.isKinematic = !enable;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
        m_rigibody.position = position;
    }

    public void SetVelocity(Vector3 velocity)
    {
        m_rigibody.velocity = velocity;
    }

    public Vector3 GetVelocity()
    {
        return m_rigibody.velocity;
    }

    private void Awake()
    {
        m_rigibody = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }
}
