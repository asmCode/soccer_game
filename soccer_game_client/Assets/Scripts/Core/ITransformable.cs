using UnityEngine;

public interface ITransformable
{
    void SetPosition(Vector3 position);
    Vector3 GetPosition();

    void SetRotation(Quaternion rotation);
    Quaternion GetRotation();
}

