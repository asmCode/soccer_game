using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerScene : MonoBehaviour
{
    private static readonly string MatchSceneName = "Match";

    private GameServer m_gameServer;
    private GameServerController m_gameServerController;
    private Match m_match;

    private void Awake()
    {
        m_gameServer = new GameServer();
        m_gameServer.PlayersConnected += HandlePlayersConnected;
        m_gameServerController = new GameServerController(m_gameServer);

        // This is for debug purpose.
        LoadMatchScene();
    }

    private void Start()
    {
        m_gameServerController.StartServer();
    }

    private void Update()
    {
        m_gameServerController.Update(Time.deltaTime);

        var movePlayer = MovePlayer.Create(Time.deltaTime, 0, 0, PlayerDirection.UpRight);
        m_match.ProcessMessage(movePlayer);
    }

    private void OnDisable()
    {
        m_gameServer.StopServer();
        SceneManager.sceneLoaded -= SceneLoaded;
    }

    private void LoadMatchScene()
    {
        m_gameServer.PlayersConnected -= HandlePlayersConnected;
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
    }

    private void HandlePlayersConnected()
    {
        LoadMatchScene();
    }
}
