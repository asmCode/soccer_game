namespace Ssg.Core.Networking
{
    public interface Connection
    {
        byte[] Data { get; }
        int DataSize { get; }

        void Send(byte[] data, int size);
        NetworkMessage GetMessage();
        void Close();
   }
}
