using System.Collections.Generic;

public class MessageSynchonizer
{
    private List<PlayerMove> m_messages = new List<PlayerMove>();

    public List<PlayerMove> Messages
    {
        get
        {
            return m_messages;
        }
    }

    public void AddMessage(PlayerMove msg)
    {
        m_messages.Add(msg);
    }

    public void DiscardProcessedMssages(int messageNumber)
    {
        int count = 0;

        foreach (var msg in m_messages)
        {
            if (msg.m_messageNumber <= messageNumber)
                count++;
            else
                break;
        }

        if (count > 0)
        {
            m_messages.RemoveRange(0, count);
            // UnityEngine.Debug.LogFormat("Removed {0} messges", count);
        }
    }
}
