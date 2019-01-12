using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerScene : MonoBehaviour
{
    private GameServer m_gameServer;
    private GameServerController m_gameServerController;

    private void Awake()
    {
        m_gameServer = new GameServer();
        m_gameServerController = new GameServerController(m_gameServer);
    }

    private void Start()
    {
        m_gameServerController.StartServer();
    }

    private void Update()
    {
        m_gameServerController.Update(Time.deltaTime);
    }

    private void OnDisable()
    {
        m_gameServer.StopServer();
    }
}
