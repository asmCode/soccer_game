using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientTester : MonoBehaviour
{
    private GameClient m_gameClient;

    private void Awake()
    {
        m_gameClient = new GameClient();
    }

    void Start()
    {

    }

    void Update()
    {
        m_gameClient.Update();
    }

    public void UIEventJoin()
    {
        m_gameClient.Join();
    }
}
