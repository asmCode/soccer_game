using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RawData
{
    public byte[] Data
    {
        get;
        private set;
    }

    public RawData(byte[] data, int size)
    {
        Data = new byte[size];
        System.Array.Copy(data, Data, size);
    }

    public void CopyTo(byte[] data, out int size)
    {
        size = Data.Length;
        System.Array.Copy(Data, data, Data.Length);
    }
}