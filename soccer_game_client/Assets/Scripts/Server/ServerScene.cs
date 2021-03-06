﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerScene : MonoBehaviour
{
    private static readonly string MatchSceneName = "Match";
    private GameServerController m_gameServerController;
    private Match m_match;
    private bool m_matchStarted;

    FakeClientOnServer m_fakeClient = new FakeClientOnServer();

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
        m_fakeClient.JoinServer();
    }

    private void Update()
    {
        m_fakeClient.Update();
        m_gameServerController.Update(Time.deltaTime);

        if (!MatchSceneLoaded())
            return;

        m_match.ClearMatchEvents();

        if (MatchSceneLoaded() && GameServer.ClientsReady() && !m_matchStarted)
        {
            m_matchStarted = true;
            GameServer.SendStartMatch();
        }

        if (m_matchStarted)
        {
            while (true)
            {
                var msg = GameServer.GetMatchMessage();
                if (msg == null)
                    break;

                m_match.ProcessMessage(msg);
            }

            m_match.Update(Time.deltaTime);

            SendPlayersPositions();
            SendBallPosition();
            SendMatchMessages();
        }
    }

    private void SendMatchMessages()
    {
        // here process slide.
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
    }

    private void InitializeMatch()
    {
        var matchScene = GameObject.Find("MatchScene").GetComponent<MatchScene>();
        m_match = matchScene.Match;
        m_match.SetLogic(new ServerMatchLogic(m_match));
    }

    private void HandlePlayersConnected()
    {
        LoadMatchScene();
    }

    private INetworkCommunication GetNetworkCommunication()
    {
        var fakeClientsGO = GameObject.Find("FakeClients");
        if (fakeClientsGO == null || !fakeClientsGO.activeSelf)
        {
            var udpCom = new UdpCommunication(GameSettings.ServerDefaultPort);
            return new DelayedNetworkCommunication(udpCom);
        }

        var fakeClients = fakeClientsGO.GetComponent<FakeClients>();

        return fakeClients.NetworkCommunication;
    }

    private bool MatchSceneLoaded()
    {
        return m_match != null;
    }

    private void SendPlayersPositions()
    {
        var players = m_match.GetPlayers();

        foreach (var player in players)
        {
            var msg = PlayerPosition.Create(GameServer.ClientInfos[0].LastMsgNum, player.Team, player.Index, player.GetPosition(), player.GetDirection());
            GameServer.m_netMsgSerializer.Serialize(msg);
            GameServer.Send(GameServer.m_netMsgSerializer.Data, GameServer.m_netMsgSerializer.DataSize, GameServer.ClientInfos[0].Address);

            msg = PlayerPosition.Create(GameServer.ClientInfos[1].LastMsgNum, player.Team, player.Index, player.GetPosition(), player.GetDirection());
            GameServer.m_netMsgSerializer.Serialize(msg);
            GameServer.Send(GameServer.m_netMsgSerializer.Data, GameServer.m_netMsgSerializer.DataSize, GameServer.ClientInfos[1].Address);
        }
    }

    private void SendBallPosition()
    {
        var ball = m_match.GetBall();

        var msg = BallPosition.Create(GameServer.ClientInfos[0].LastMsgNum, ball.GetPosition(), ball.GetVelocity());
        GameServer.m_netMsgSerializer.Serialize(msg);
        GameServer.Send(GameServer.m_netMsgSerializer.Data, GameServer.m_netMsgSerializer.DataSize, GameServer.ClientInfos[0].Address);

        msg = BallPosition.Create(GameServer.ClientInfos[1].LastMsgNum, ball.GetPosition(), ball.GetVelocity());
        GameServer.m_netMsgSerializer.Serialize(msg);
        GameServer.Send(GameServer.m_netMsgSerializer.Data, GameServer.m_netMsgSerializer.DataSize, GameServer.ClientInfos[1].Address);
    }
}
