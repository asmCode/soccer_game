namespace ssg
{
    public interface ICollider
    {
        ColliderId ColliderId { get; }
        void NotifyCollision(ICollider otherCollider);
    }
}
