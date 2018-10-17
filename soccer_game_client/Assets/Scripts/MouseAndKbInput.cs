using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseAndKbInput : UserInput
{
    public override PlayerDirection Direction { get; protected set; }
    public override bool Shoot { get; protected set; }

    private void Update()
    {
        Direction = PlayerDirection.None;

        if (Input.GetKey(KeyCode.W))
        {
            Direction = PlayerDirection.Up;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Shoot = true;
        }
    }
}
