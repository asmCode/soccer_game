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
    private MatchInput m_matchInput;

    private void Awake()
    {
        m_userInput = new MouseAndKbInput();
        m_matchInput = new MatchInput(m_userInput);

        LoadMatchScene();
    }

    private void Start()
    {
    }

    private void Update()
    {
        if (!m_matchStarted)
            return;

        m_matchInput.Update(Time.deltaTime);

        var direction = m_matchInput.GetDirection();
        if (direction != PlayerDirection.None)
        {
            m_match.Run(0, 0, direction, Time.deltaTime);
        }
        else if (m_matchInput.DirectionChanged())
        {
            m_match.StopRunning(0, 0);
        }

        if (m_matchInput.GetAction())
        {
            m_match.PlayerAction(0, m_matchInput.GetActionDuration());
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
        m_match.SetLogic(new LocalMatchLogic(m_match));

        m_matchStarted = true;
    }
}
