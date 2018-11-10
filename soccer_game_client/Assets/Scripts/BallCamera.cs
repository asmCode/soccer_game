using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCamera : MonoBehaviour
{
    public BallView m_ball;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (m_ball == null)
            return;

        var position = transform.position;
        position.x = m_ball.transform.position.x;
        position.z = m_ball.transform.position.z;
        transform.position = position;
    }
}
