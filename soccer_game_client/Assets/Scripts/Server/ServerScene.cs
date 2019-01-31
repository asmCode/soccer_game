﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerScene : MonoBehaviour
{
    private static readonly string MatchSceneName = "Match";
    private GameServerController m_gameServerController;
    private Match m_match;

    public GameServer GameServer
    {
        get;
        private set;
    }


    private void Awake()
    {
        var netCom = GetNetworkCommunication();
        GameServer = new GameServer(netCom);
        GameServer.PlayersConnected += HandlePlayersConnected;
        m_gameServerController = new GameServerController(GameServer);

        // This is for debug purpose.
        // LoadMatchScene();
    }

    private void Start()
    {
        m_gameServerController.StartServer();
    }

    private void Update()
    {
        m_gameServerController.Update(Time.deltaTime);

        if (!MatchSceneLoaded())
            return;

        if (MatchSceneLoaded() && GameServer.ClientsReady())
            Debug.Log("dupsko");

        var movePlayer = MovePlayer.Create(Time.deltaTime, 0, 0, PlayerDirection.UpRight);
        m_match.ProcessMessage(movePlayer);
    }

    private void OnDisable()
    {
        GameServer.StopServer();
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void LoadMatchScene()
    {
        GameServer.PlayersConnected -= HandlePlayersConnected;
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene(MatchSceneName, LoadSceneMode.Additive);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != MatchSceneName)
            return;

        Debug.Log("Match scene loaded.");

        InitializeMatch();

        // Here you have to wait for ReadyToStart from both clients and send StartMatch message.
    }

    private void InitializeMatch()
    {
        var matchScene = GameObject.Find("MatchScene").GetComponent<MatchScene>();
        m_match = matchScene.Match;
    }

    private void HandlePlayersConnected()
    {
        LoadMatchScene();
    }

    private INetworkCommunication GetNetworkCommunication()
    {
        var fakeClientsGO = GameObject.Find("FakeClients");
        if (fakeClientsGO == null || !fakeClientsGO.activeSelf)
            return new UdpCommunication(GameSettings.ServerDefaultPort);

        var fakeClients = fakeClientsGO.GetComponent<FakeClients>();

        return fakeClients.NetworkCommunication;
    }

    private bool MatchSceneLoaded()
    {
        return m_match != null;
    }
}
