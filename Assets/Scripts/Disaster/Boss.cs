using UnityEngine;


namespace DefaultNamespace
{
    // This is an abstract base-class for the big disaster bosses
    // They will probably have a set of attacks they can do
    // They will probably have a large health bar and custom gimmicks
    public abstract class Boss : MonoBehaviour
    {
        [SerializeField] protected WaveSpawner waveSpawner;

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            if (waveSpawner.currentWaveIndex >= waveSpawner.waves.Length)
            {
                Die();
            }
        }

        protected void Die()
        {
            Debug.Log($"The Disaster {gameObject.name} was defeated.");
            Destroy(gameObject); //remove the boss
            Destroy(waveSpawner);
        }
    }
}
