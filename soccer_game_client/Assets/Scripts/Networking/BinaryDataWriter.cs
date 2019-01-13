using System.IO;
using UnityEngine;

public class BinaryDataWriter : IDataWriter
{
    private static byte[] Buffer = new byte[256];
    private static MemoryStream m_stream = new MemoryStream(Buffer);
    private static BinaryWriter m_reader = new BinaryWriter(m_stream);

    public byte[] Data
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public int DataSize
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public BinaryDataWriter()
    {
        m_stream.Seek(0, SeekOrigin.Begin);
    }

    public void Reset()
    {
        throw new System.NotImplementedException();
    }

    public void Flush()
    {
        throw new System.NotImplementedException();
    }

    public void Write(NetworkMessageType data)
    {
        throw new System.NotImplementedException();
    }

    public void Write(string data)
    {
        throw new System.NotImplementedException();
    }

    public void Write(byte data)
    {
        throw new System.NotImplementedException();
    }

    public void Write(short data)
    {
        throw new System.NotImplementedException();
    }

    public void Write(int data)
    {
        throw new System.NotImplementedException();
    }

    public void Write(float data)
    {
        throw new System.NotImplementedException();
    }

    public void Write(Vector3 data)
    {
        throw new System.NotImplementedException();
    }
}
