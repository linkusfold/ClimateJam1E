using UnityEngine;


namespace DefaultNamespace
{
    // This is an abstract base-class for the big disaster bosses
    // Their attacks are mainly handled by the wave spawner
    // This class is for any special attacks/behavior that happen outside of the wave system
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
