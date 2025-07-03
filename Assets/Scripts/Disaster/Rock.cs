using UnityEngine;

namespace DefaultNamespace
{
    public class Rock : Enemy
    // Volcano attack; falls down and does big damage
    {
        public float despawnY = -10f; // The Y at which point the attack will
        
        protected override void Start()
        {
            speed = 2;
            health = 150;
            damage = 50;

            waveSpawner = GetComponentInParent<WaveSpawner>();
        }

        // We override the update logic of enemy because these will not follow a path
        //This may be temporarily, later we may make them a custom path
        protected override void FixedUpdate()
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime);

            // If below the world-space threshold, die
            if (transform.position.y < despawnY)
            {
                Die();
            }
        }

        protected override void Attack()
        {
            Debug.Log("Rock smash!");
            // Custom logic here for the rock doing big damage to a house/tower
        }

    }
}