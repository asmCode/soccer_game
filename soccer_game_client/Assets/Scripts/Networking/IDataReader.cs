using UnityEngine;

public interface IDataReader
{
    void Reset(byte[] data, int size);

    void Read(out NetworkMessageType data);
    void Read(out string data);
    void Read(out byte data);
    void Read(out short data);
    void Read(out int data);
    void Read(out float data);
    void Read(out Vector3 data);
}
