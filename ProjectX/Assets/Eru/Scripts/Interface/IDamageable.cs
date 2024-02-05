public interface IDamageable
{
    int Health { get; }
    void TakeDamage(int damage, float shock, float inv);

}