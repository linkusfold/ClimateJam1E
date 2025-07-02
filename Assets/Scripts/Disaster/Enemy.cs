using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Enemy : MonoBehaviour
    // This is an abstract base-class for all disaster attacks.
    // Attacks are vary general, all they have is a speed and a unique attack.
    {
        protected float speed = 1;
        protected float health = 100;
        protected float damage = 10; //The amount of damage it's attack does
        protected WaveSpawner waveSpawner;


        protected virtual void Start()
        {
            waveSpawner = GetComponentInParent<WaveSpawner>();
        }

        protected abstract void FixedUpdate();

        public void TakeDamage(float amount)
        {
            health -= amount;

            Debug.Log("Enemy " + gameObject.name + " health reduced to " + health + "!");

            if (health <= 0)
            {
                Die();
            }
        }

        protected abstract void Attack(); //attack method to be overriden by subclasses
        // we add specific attack behavior in the subclasses

        protected void Die()
        {
            Debug.Log($"{gameObject.name} died.");
            Destroy(gameObject); //remove the attack-object
        }
    }
}