namespace Assets
{
    public interface IDamageable<T>
    {
        void Damage(T damageTaken);
    }
}
