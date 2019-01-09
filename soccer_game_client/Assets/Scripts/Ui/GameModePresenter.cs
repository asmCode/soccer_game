using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModePresenter : MonoBehaviour
{
    public void UIEventStartServer()
    {
        Game.Get().StartServer();
    }

    public void UIEventFindOnlineMatch()
    {
        Debug.Log("online");
    }

    public void UIEventLocal()
    {
        Debug.Log("local");
    }
}
