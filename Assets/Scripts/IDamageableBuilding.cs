namespace DefaultNamespace
{
    public interface IDamageableBuilding
    {
        public bool IsDestroyed { set; get; }
        void TakeDamage(int damage);
    }
}