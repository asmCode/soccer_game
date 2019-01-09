using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameModePresenter : MonoBehaviour
{
    public void UIEventStartServer()
    {
        Debug.Log("server");
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
