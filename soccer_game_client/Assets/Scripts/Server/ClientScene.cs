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
    }

    private INetworkCommunication GetNetworkCommunication()
    {
        var fakeServerGO = GameObject.Find("FakeServer");
        if (fakeServerGO == null || !fakeServerGO.activeSelf)
            return new UdpCommunication(0);

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
            SendPlayerMoveMessage(moveMessage);
    }

    private void SendPlayerMoveMessage(PlayerMove msg)
    {
        GameClient.m_netMsgSerializer.Serialize(msg);
        GameClient.Send(GameClient.m_netMsgSerializer.Data, GameClient.m_netMsgSerializer.DataSize);
    }
}
