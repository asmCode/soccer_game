using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientScene : MonoBehaviour
{
    private static readonly string MatchSceneName = "Match";
    private Match m_match;
    private bool m_matchStarted;

    public GameClient GameClient
    {
        get;
        private set;
    }

    private void Awake()
    {
        var netCom = GetNetworkCommunication();
        GameClient = new GameClient(netCom);
        GameClient.OpponentFound += HandleOpponentFound;

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
}
