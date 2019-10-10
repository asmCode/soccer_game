namespace ssg
{
    public abstract class Collider
    {
        public event CollisionDelegate Collision;

        public abstract ColliderId ColliderId { get; }

        public void NotifyCollision(Collider otherCollider)
        {
            if (Collision != null)
                Collision(otherCollider);
        }
    }
}
