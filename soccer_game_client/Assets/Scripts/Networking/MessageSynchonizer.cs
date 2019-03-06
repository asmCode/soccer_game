using System.Collections.Generic;

public class MessageSynchonizer
{
    private List<NetworkMessage> m_messages = new List<NetworkMessage>();

    public List<NetworkMessage> Messages
    {
        get
        {
            return m_messages;
        }
    }

    public void AddMessage(NetworkMessage msg)
    {
        m_messages.Add(msg);
    }

    public void DiscardProcessedMssages(int messageNumber)
    {
        int count = 0;

        foreach (var msg in m_messages)
        {
            if (msg.m_number <= messageNumber)
                count++;
            else
                break;
        }

        if (count > 0)
            m_messages.RemoveRange(0, count);
    }
}
