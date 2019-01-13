using System.IO;
using UnityEngine;

public class BinaryDataReader : IDataReader
{
    private MemoryStream m_stream;
    private BinaryReader m_reader;

    public void Reset(byte[] data, int size)
    {
        if (m_stream != null)
            m_stream.Close();

        m_stream = new MemoryStream(data, 0, size);
        m_reader = new BinaryReader(m_stream);
    }

    public void Read(out NetworkMessageType data)
    {
        data = (NetworkMessageType)m_reader.ReadByte();
    }

    public void Read(out string data)
    {
        data = m_reader.ReadString();
    }

    public void Read(out byte data)
    {
        data = m_reader.ReadByte();
    }

    public void Read(out short data)
    {
        data = m_reader.ReadInt16();
    }

    public void Read(out int data)
    {
        data = m_reader.ReadInt32();
    }

    public void Read(out float data)
    {
        data = m_reader.ReadSingle();
    }

    public void Read(out Vector3 data)
    {
        data.x = m_reader.ReadSingle();
        data.y = m_reader.ReadSingle();
        data.z = m_reader.ReadSingle();
    }
}
