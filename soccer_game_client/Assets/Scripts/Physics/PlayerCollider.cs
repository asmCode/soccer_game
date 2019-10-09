using UnityEngine;

namespace ssg
{
    public class PlayerCollider : MonoBehaviour, ICollider
    {
        public ColliderId ColliderId
        {
            get
            {
                return ColliderId.Player;
            }
        }

        public event CollistionDelegate OnCollision;

        private void Awake()
        {
            void 
        }
    }
}
