using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayer : ITransformable
{
    byte Team { get; }
    byte Index { get; }

    PlayerState State { get; set; }

    float SlideTime { get; set; }
    Vector3 SlideBasePos { get; set; }

    void Update(float deltaTime);

    void OffsetPosition(Vector3 offset);
    void Run(PlayerDirection playerDirection, float deltaTime);

    void SetDirection(PlayerDirection direction);
    PlayerDirection GetDirection();

    Vector3 GetDirectionVector();

    void PlayAnimation(PlayerAnimationType playerAnimationType);

    void SetIdle();

    void Slide();
}
