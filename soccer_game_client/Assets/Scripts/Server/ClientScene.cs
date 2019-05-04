using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientScene : MonoBehaviour
{
    private static readonly string MatchSceneName = "Match";
    private Match m_match;
    private bool m_matchStarted;
    private MatchInputProcessor m_inputProc;
    private MessageSynchonizer m_msgSync = new MessageSynchonizer();

    private int m_clientMessageNumber;

    public GameClient GameClient
    {
        get;
        private set;
    }

    private void Awake()
    {
        m_inputProc = new MatchInputProcessor(new MouseAndKbInput());

        var netCom = GetNetworkCommunication();
        GameClient = new GameClient(netCom);
        GameClient.OpponentFound += HandleOpponentFound;
        GameClient.MatchStarted += HandleMatchStarted;

        // This is for debug purpose.
        // LoadMatchScene();
    }

    private void Start()
    {
    }

    private void Update()
    {
        GameClient.Update();

        if (!MatchSceneLoaded())
            return;

        if (m_matchStarted)
        {
            UpdateInput(Time.deltaTime);

            while (true)
            {
                var msg = GameClient.GetMatchMessage();
                if (msg == null)
                    break;

                m_match.ProcessMessage(msg);

                if (msg.m_messageType == MessageType.BallPosition)
                {
                    var ballPosMsg = (BallPosition)msg.m_message;
                    m_msgSync.DiscardProcessedMssages(ballPosMsg.m_clientMsgNum);
                }

                if (msg.m_messageType == MessageType.PlayerPosition)
                {
                    var playerPosMsg = (PlayerPosition)msg.m_message;
                    m_msgSync.DiscardProcessedMssages(playerPosMsg.m_clientMsgNum);

                    for (int i = 0; i < m_msgSync.Messages.Count; i++)
                    {
                        if (playerPosMsg.m_index == m_msgSync.Messages[i].m_playerIndex &&
                            playerPosMsg.m_team == m_msgSync.Messages[i].m_team)
                        {
                            var matchMessage = new MatchMessage();
                            matchMessage.m_messageType = MessageType.PlayerMove;
                            matchMessage.m_message = m_msgSync.Messages[i];

                            m_match.ProcessMessage(matchMessage);
                        }
                    }
                }
            }
        }
    }

    private void OnDisable()
    {
        GameClient.Disconnect();
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void LoadMatchScene()
    {
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene(MatchSceneName, LoadSceneMode.Additive);
    }

    private void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name != MatchSceneName)
            return;

        Debug.Log("Match scene loaded.");

        InitializeMatch();

        GameClient.SendReadyToStart();
    }

    private void InitializeMatch()
    {
        var matchScene = GameObject.Find("MatchScene").GetComponent<MatchScene>();
        m_match = matchScene.Match;
        m_match.SetLogic(new ClientMatchLogic(m_match));
    }

    private INetworkCommunication GetNetworkCommunication()
    {
        var fakeServerGO = GameObject.Find("FakeServer");
        if (fakeServerGO == null || !fakeServerGO.activeSelf)
        {
            var udpCom = new UdpCommunication(0);
            return new DelayedNetworkCommunication(udpCom);
        }


        var fakeServer = fakeServerGO.GetComponent<FakeServer>();

        return fakeServer.NetworkCommunication;
    }

    private bool MatchSceneLoaded()
    {
        return m_match != null;
    }

    public void UiEventJoinServer()
    {
        GameClient.Join();
    }

    private void HandleOpponentFound()
    {
        LoadMatchScene();
    }

    private void HandleMatchStarted()
    {
        m_matchStarted = true;
    }

    private void UpdateInput(float deltaTime)
    {
        if (m_inputProc == null)
            return;

        m_inputProc.Update(deltaTime);

        var moveMessage = m_inputProc.GetMoveMessage();
        if (moveMessage != null)
        {
            m_clientMessageNumber++;
            moveMessage.m_messageNumber = m_clientMessageNumber;
            SendPlayerMoveMessage(moveMessage);

            moveMessage.m_team = GameClient.Team;
            m_msgSync.AddMessage(moveMessage);

        }

        float actionDuration;
        if (m_inputProc.GetAction(out actionDuration))
            SendPlayerActionMessage(actionDuration);
    }

    private void SendPlayerMoveMessage(PlayerMove msg)
    {
        GameClient.m_netMsgSerializer.Serialize(msg);
        GameClient.Send(GameClient.m_netMsgSerializer.Data, GameClient.m_netMsgSerializer.DataSize);
    }

    private void SendPlayerActionMessage(float duration)
    {
        var msg = new Action();
        msg.m_duration = duration;
        GameClient.m_netMsgSerializer.Serialize(msg);
        GameClient.Send(GameClient.m_netMsgSerializer.Data, GameClient.m_netMsgSerializer.DataSize);
    }
}
