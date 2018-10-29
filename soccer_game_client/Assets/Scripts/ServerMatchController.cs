using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerMatchController : MatchController
{
    private Match m_match;

    public ServerMatchController(Match match)
    {
        m_match = match;
    }

    public void Cleanup()
    {
    }

    public void Update(float deltaTime)
    {
    }
}
