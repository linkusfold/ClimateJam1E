using UnityEngine;


namespace DefaultNamespace
{
    public abstract class Boss : MonoBehaviour
    // This is an abstract base-class for the big disaster bosses
    // They will probably have a set of attacks they can do
    // They will probably have a large health bar and custom gimmicks
    {
        protected float health = 500;
        protected float defense = 10; //amount oncoming damage is reduced

        protected void Start() { }

        protected void Update() { }

        public void TakeDamage(float amount)
        {
            //First reduce incoming damage by the defense amount
            float effectiveDamage = Mathf.Max(amount - defense, 0);
            health -= effectiveDamage;

            Debug.Log("Disaster " + gameObject.name + " health reduced to " + health + "!");

            if (health <= 0)
            {
                Die();
            }
        }

        protected void Die()
        {
            Debug.Log($"The Disaster {gameObject.name} was defeated.");
            Destroy(gameObject); //remove the boss
        }
    }
}
