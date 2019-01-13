using UnityEngine;

public interface IDataWriter
{
    byte[] Data { get; }
    int DataSize { get; }

    void Reset();
    void Flush();
    void Write(NetworkMessageType data);
    void Write(string data);
    void Write(byte data);
    void Write(short data);
    void Write(int data);
    void Write(float data);
    void Write(Vector3 data);
}
