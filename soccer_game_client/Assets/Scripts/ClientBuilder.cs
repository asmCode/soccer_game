using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientBuilder
{
    public static MatchController Create(Match match, MatchView matchView)
    {
        var userInput = new MouseAndKbInput();

        var ctrl = new ClientMatchController(match, userInput);
        matchView.Init(match);

        return ctrl;
    }
}
