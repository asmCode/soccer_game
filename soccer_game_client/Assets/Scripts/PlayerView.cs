﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    public Vector3 Position
    {
        get { return transform.position; }
        set { transform.position = value; }
    }
}
