using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class TextMessageSerializer : MessageSerializer
{
    public override MatchMessage Deserialize(byte[] data)
    {
        var text = Encoding.ASCII.GetString(data);

        var command = text.Split(' ');
        Debug.Assert(command.Length > 0);

        if (command[0] == "Action")
        {
            Debug.Assert(command.Length == 4);

            return Action.Create(
                byte.Parse(command[1].Trim()),
                byte.Parse(command[2].Trim()),
                float.Parse(command[3].Trim()));
        }
        else if (command[0] == "BallPosition")
        {
            Debug.Assert(command.Length == 7);

            var position = new Vector3(
                float.Parse(command[1].Trim()),
                float.Parse(command[2].Trim()),
                float.Parse(command[3].Trim()));

            var velocity = new Vector3(
                float.Parse(command[4].Trim()),
                float.Parse(command[5].Trim()),
                float.Parse(command[6].Trim()));

            // return BallPosition.Create(position, velocity);
            return null;
        }
        else if (command[0] == "PlayerMove")
        {
            Debug.Assert(command.Length == 5);

            //return PlayerMove.Create(
            //    float.Parse(command[1].Trim()),
            //    byte.Parse(command[2].Trim()),
            //    byte.Parse(command[3].Trim()),
            //    (PlayerDirection)byte.Parse(command[4].Trim()));

            return null;
        }
        else if (command[0] == "PlayerPosition")
        {
            Debug.Assert(command.Length == 7);

            var position = new Vector3(
                    float.Parse(command[3].Trim()),
                    float.Parse(command[4].Trim()),
                    float.Parse(command[5].Trim()));

            //return PlayerPosition.Create(
            //    byte.Parse(command[1].Trim()),
            //    byte.Parse(command[2].Trim()),
            //    position,
            //    (PlayerDirection)byte.Parse(command[6].Trim()));

            return null;
        }

        return null;
    }

    public override byte[] Serialize(MatchMessage message)
    {
        Application.targetFrameRate = 60;
        string serialized = "";

        switch (message.m_messageType)
        {
            case MessageType.PlayerAction:
                var action = message.m_message as Action;
                serialized = string.Format("Action {0} {1} {2:0.000}",
                    action.m_team,
                    action.m_playerIndex,
                    action.m_duration);
                break;

            case MessageType.BallPosition:
                var ballPosition = message.m_message as BallPosition;
                serialized = string.Format("BallPosition {0:0.0000} {1:0.0000} {2:0.0000}",
                    ballPosition.m_position.x,
                    ballPosition.m_position.y,
                    ballPosition.m_position.z);
                break;

            case MessageType.PlayerMove:
                var playerMove = message.m_message as PlayerMove;
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
