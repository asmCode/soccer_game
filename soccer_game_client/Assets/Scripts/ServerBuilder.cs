using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerBuilder
{
    public static MatchController Create(Match match)
    {
        return new ServerMatchController(match);
    }
}
