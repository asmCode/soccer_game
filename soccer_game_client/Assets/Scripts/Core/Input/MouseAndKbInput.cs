using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndKbInput : UserInput
{
    public override PlayerDirection GetDirection()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.A))
                return PlayerDirection.UpLeft;
            if (Input.GetKey(KeyCode.D))
                return PlayerDirection.UpRight;
            return PlayerDirection.Up;
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (Input.GetKey(KeyCode.A))
                return PlayerDirection.BottomLeft;
            if (Input.GetKey(KeyCode.D))
                return PlayerDirection.BottomRight;
            return PlayerDirection.Bottom;
        }

        if (Input.GetKey(KeyCode.A))
            return PlayerDirection.Left;

        if (Input.GetKey(KeyCode.D))
            return PlayerDirection.Right;

        return PlayerDirection.None;
    }

    public override bool GetAction()
    {
        return Input.GetKey(KeyCode.Space);
    }
}
