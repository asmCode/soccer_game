using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServerAddressGetter
{
    INetworkAddress GetServerAddress();
}
