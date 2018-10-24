using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UserInput
{
    public abstract PlayerDirection GetDirection();
    public abstract bool GetAction();
}
