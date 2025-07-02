using UnityEngine;


namespace DefaultNamespace
{
    public abstract class Boss : MonoBehaviour
    // This is an abstract base-class for the big disaster bosses
    // They will probably have a set of attacks they can do
    // They will probably have a large health bar and custom gimmicks
    {
        [SerializeField] private WaveSpawner waveSpawner;

        protected void Start()
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
