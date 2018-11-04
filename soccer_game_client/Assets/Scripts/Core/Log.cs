using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log
{
    public static void Message(string message, params object[] args)
    {
        Debug.LogFormat(message, args);
    }
}
