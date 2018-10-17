using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserInput : MonoBehaviour
{
    public abstract PlayerDirection Direction { get; protected set; }
    public abstract bool Shoot { get; protected set; }
}
