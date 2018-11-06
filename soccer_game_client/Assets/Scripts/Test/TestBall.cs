using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBall : MonoBehaviour
{

    private Rigidbody m_rigibody;

    // Use this for initialization
    void Awake()
    {
        m_rigibody = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 vec = Vector3.forward * 6 + Vector3.up * 3.0f;
            //m_rigibody.velocity = vec;
            m_rigibody.AddForce(vec, ForceMode.Impulse);
            // m_rigibody.velocity = Vector3.forward * 5.0f;
        }
    }
}
