namespace Ssg.Core.Networking
{
   public interface Connection
   {
        void Send(Message message);
        Message GetMessage();
        void Close();
   }
}
