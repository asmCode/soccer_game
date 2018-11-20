using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer
{
    byte Team { get; }
    byte Index { get; }

    Vector3 GetPosition();
    void SetPosition(Vector3 position);

    void SetDirection(PlayerDirection direction);
    PlayerDirection GetDirection();
}
