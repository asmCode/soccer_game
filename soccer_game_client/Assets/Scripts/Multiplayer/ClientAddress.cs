using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ClientAddress
{
    public abstract ClientAddress Clone();
    public abstract string GetAddressName();
}
