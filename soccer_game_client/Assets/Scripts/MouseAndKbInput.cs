using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndKbInput : UserInput
{
    public override PlayerDirection GetDirection()
    {
        if (Input.GetKey(KeyCode.W))
        {
            return PlayerDirection.Up;
        }

        return PlayerDirection.None;
    }

    public override bool GetAction()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
