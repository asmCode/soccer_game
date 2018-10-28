using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirectionVector
{
    public static readonly Vector3 None = Vector3.zero;
    public static readonly Vector3 Up = Vector3.forward;
    public static readonly Vector3 UpRight = (Vector3.forward + Vector3.right).normalized;
    public static readonly Vector3 Right = Vector3.right;
    public static readonly Vector3 BottomRight = (Vector3.back + Vector3.right).normalized;
    public static readonly Vector3 Bottom = Vector3.back;
    public static readonly Vector3 BottomLeft = (Vector3.back + Vector3.left).normalized;
    public static readonly Vector3 Left = Vector3.left;
    public static readonly Vector3 UpLeft = (Vector3.forward + Vector3.left).normalized;

    public static readonly Vector3[] Vectors =
    {
        None,
        Up,
        UpRight,
        Right,
        BottomRight,
        Bottom,
        BottomLeft,
        Left,
        UpLeft
    };

    public static Vector3 GetVector(PlayerDirection direction)
    {
        return Vectors[(int)direction];
    }
}
