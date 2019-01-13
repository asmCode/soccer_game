using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INetworkCommunication
{
    void Initialize();
    void Send(byte[] data, int size, INetworkAddress address);
    bool Receive(byte[] data, out int size, out INetworkAddress address);
    void Close();
}
