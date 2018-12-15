namespace Ssg.Core.Networking
{
   public interface Connection
   {
        void Send(NetworkMessage message);
        NetworkMessage GetMessage();
        void Close();
   }
}
