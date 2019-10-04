using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LocalGameScene : MonoBehaviour
{
    private static readonly string MatchSceneName = "Match";
    private Match m_match;
    private bool m_matchStarted;
    private UserInput m_userInput;

    private void Awake()
    {
        m_userInput = new MouseAndKbInput();

        LoadMatchScene();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (!m_matchStarted)
            return;

        var directionType = m_userInput.GetDirection();
        if (directionType != PlayerDirection.None)
        {
            // var direction = PlayerDirectionVector.GetVector(directionType);
            m_match.Run(0, 0, directionType, Time.deltaTime);
        }
        else
        {
            m_match.StopRunning(0, 0);
        }

        if (m_userInput.GetAction())
        {
            m_match.PlayerAction(0, 0);
        }
    }

    private void OnDisable()
    {
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
    }

    private void InitializeMatch()
    {
        var matchScene = GameObject.Find("MatchScene").GetComponent<MatchScene>();
        m_match = matchScene.Match;
        m_match.SetLogic(new ClientMatchLogic(m_match));

        m_matchStarted = true;
    }
}
