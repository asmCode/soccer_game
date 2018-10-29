using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface MatchController
{
    void Update(float deltaTime);
    void Cleanup();
}
