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

    private void Awake()
    {
        PlayerProps.Instance = new PlayerProps(0.5f);
        Match = new Match();
    }

    private void Start()
    {

    }

    private void Update()
    {
        
    }
}
