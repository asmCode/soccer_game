using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatchScene : MonoBehaviour
{
    public Match Match
    {
        get;
        private set;
    }

    public MatchView MatchView
    {
        get;
        private set;
    }

    private void Awake()
    {
        PlayerProps.Instance = new PlayerProps(0.5f);
        Match = new Match();

        MatchView = GameObject.Find("MatchView").GetComponent<MatchView>();
        MatchView.Init(Match);
    }

    private void Start()
    {

    }

    private void Update()
    {
        Match.Update(Time.deltaTime);
    }
}
