using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class TextMessageSerializer : MessageSerializer
{
    public override Message Deserialize(byte[] data)
    {
        var text = Encoding.ASCII.GetString(data);

        var command = text.Split(' ');
        Debug.Assert(command.Length > 0);

        if (command[0] == "PlayerMove")
        {
            Debug.Assert(command.Length == 5);

            return MovePlayer.Create(
                float.Parse(command[1].Trim()),
                byte.Parse(command[2].Trim()),
                byte.Parse(command[3].Trim()),
                (PlayerDirection)byte.Parse(command[4].Trim()));
        }

        return null;
    }

    public override byte[] Serialize(Message message)
    {
        Application.targetFrameRate = 60;
        string serialized = "";

        switch (message.m_messageType)
        {
            case MessageType.PlayerMove:
                var playerMove = message.m_message as MovePlayer;
                serialized = string.Format("PlayerMove {0:0.000} {1} {2} {3}",
                    playerMove.m_dt,
                    playerMove.m_team,
                    playerMove.m_playerIndex,
                    (byte)playerMove.m_playerDirection);
                break;
        }

        if (!string.IsNullOrEmpty(serialized))
            return Encoding.ASCII.GetBytes(serialized);

        return null;
    }
}
