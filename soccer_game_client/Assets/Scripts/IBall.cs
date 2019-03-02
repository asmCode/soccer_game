using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBall
{
    IPlayer GetPlayer();
    void SetPlayer(IPlayer player);
    void ClearPlayer();

    void EnablePhysics(bool enable);
    Vector3 GetPosition();
    void SetPosition(Vector3 position);

    Vector3 GetVelocity();
    void SetVelocity(Vector3 velocity);
}
