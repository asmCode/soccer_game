using System.IO;
using UnityEngine;

public class BinaryDataWriter : IDataWriter
{
    private byte[] m_data = new byte[256];
    private MemoryStream m_stream;
    private BinaryWriter m_writer;

    public byte[] Data
    {
        get
        {
            return m_data;
        }
    }

    public int DataSize
    {
        get
        {
            return (int)m_stream.Position;
        }
    }

    public BinaryDataWriter()
    {
        m_data = new byte[256];
        m_stream = new MemoryStream(m_data);
        m_writer = new BinaryWriter(m_stream);
    }

    public void Reset()
    {
        m_stream.Seek(0, SeekOrigin.Begin);
    }

    public void Flush()
    {
        m_writer.Flush();
    }

    public void Write(NetworkMessageType data)
    {
        m_writer.Write((byte)data);
    }

    public void Write(string data)
    {
        m_writer.Write(data);
    }

    public void Write(byte data)
    {
        m_writer.Write(data);
    }

    public void Write(short data)
    {
        m_writer.Write(data);
    }

    public void Write(int data)
    {
        m_writer.Write(data);
    }

    public void Write(float data)
    {
        m_writer.Write(data);
    }

    public void Write(Vector3 data)
    {
        m_writer.Write(data.x);
        m_writer.Write(data.y);
        m_writer.Write(data.z);
    }
}
