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

        if (command[0] == "Action")
        {
            Debug.Assert(command.Length == 3);

            return Action.Create(
                byte.Parse(command[1].Trim()),
                float.Parse(command[2].Trim()));
        }
        else if (command[0] == "PlayerMove")
        {
            Debug.Assert(command.Length == 5);

            return MovePlayer.Create(
                float.Parse(command[1].Trim()),
                byte.Parse(command[2].Trim()),
                byte.Parse(command[3].Trim()),
                (PlayerDirection)byte.Parse(command[4].Trim()));
        }
        else if (command[0] == "PlayerPosition")
        {
            Debug.Assert(command.Length == 7);

            var position = new Vector3(
                    float.Parse(command[3].Trim()),
                    float.Parse(command[4].Trim()),
                    float.Parse(command[5].Trim()));

            return PlayerPosition.Create(
                byte.Parse(command[1].Trim()),
                byte.Parse(command[2].Trim()),
                position,
                (PlayerDirection)byte.Parse(command[6].Trim()));
        }

        return null;
    }

    public override byte[] Serialize(Message message)
    {
        Application.targetFrameRate = 60;
        string serialized = "";

        switch (message.m_messageType)
        {
            case MessageType.PlayerAction:
                var action = message.m_message as Action;
                serialized = string.Format("Action {0} {1:0.000}",
                    action.m_team,
                    action.m_duration);
                break;

            case MessageType.PlayerMove:
                var playerMove = message.m_message as MovePlayer;
                serialized = string.Format("PlayerMove {0:0.000} {1} {2} {3}",
                    playerMove.m_dt,
                    playerMove.m_team,
                    playerMove.m_playerIndex,
                    (byte)playerMove.m_playerDirection);
                break;

            case MessageType.PlayerPosition:
                var playerPosition = message.m_message as PlayerPosition;
                serialized = string.Format("PlayerPosition {0} {1} {2:0.0000} {3:0.0000} {4:0.0000} {5}",
                    playerPosition.m_team,
                    playerPosition.m_index,
                    playerPosition.m_position.x,
                    playerPosition.m_position.y,
                    playerPosition.m_position.z,
                    (byte)playerPosition.m_direction);
                break;
        }

        if (!string.IsNullOrEmpty(serialized))
            return Encoding.ASCII.GetBytes(serialized);

        return null;
    }
}
