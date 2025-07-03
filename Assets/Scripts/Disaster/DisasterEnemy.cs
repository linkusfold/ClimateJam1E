using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    // This is an abstract base-class for all disaster attacks.
    // Attacks are very general, all they have is a speed and a unique attack.
    public abstract class DisasterEnemy : MonoBehaviour
    {
        protected float speed = 1;
        [SerializeField] private HealthBar healthBarPrefab;
        private HealthBar healthBar;
        protected float health = 100f;
        protected float damage = 10; //The amount of damage it's attack does
        protected WaveSpawner waveSpawner;


        protected virtual void Start()
        {
            waveSpawner = GetComponentInParent<WaveSpawner>();

            // Instantiate health bar slightly above the enemy
            if (healthBarPrefab != null) //the enemy may not display it's health
            {
                Vector3 offset = new Vector3(0, 0.7f, 0);
                healthBar = Instantiate(healthBarPrefab, transform.position + offset, Quaternion.identity, transform);
                healthBar.MaxHealth = health;
            }
        }

        protected abstract void FixedUpdate();

        public void TakeDamage(float amount)
        {
            health -= amount;
            if (healthBar != null) //display health only if there is a healtbar
                healthBar.Health = health;

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
            waveSpawner.levelData.waves[waveSpawner.levelData.currentWaveIndex].enemiesLeft--;
            Debug.Log($"{gameObject.name} died.");
            Destroy(gameObject); //remove the attack-object
        }
    }
}