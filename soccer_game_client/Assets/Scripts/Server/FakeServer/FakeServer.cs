using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeServer : MonoBehaviour
{
    private GameServer m_gameServer;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        m_gameServer = GameObject.Find("ServerScene").GetComponent<ServerScene>().GameServer;
    }
}
