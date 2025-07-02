using UnityEngine;

namespace DefaultNamespace
{
    public class Hurricane : Boss
    {
        protected float health = 500;

        public void TakeDamage(float amount)
        {
            health -= amount;

            Debug.Log("Disaster " + gameObject.name + " health reduced to " + health + "!");

            if (health <= 0)
            {
                Die();
            }
        }
    }
}