﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    public List<IPlayer> Players = new List<IPlayer>();
    public int ActivePlayerIndex;
}
