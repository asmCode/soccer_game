using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Updater : MonoBehaviour
{
    private System.Action m_action;

    public static Updater Create(string name, System.Action action)
    {
        var go = new GameObject(name);
        var updater = go.AddComponent<Updater>();
        updater.m_action = action;
        return updater;
    }

    private void Update()
    {
        m_action();
    }
}
