using UnityEngine;

namespace DefaultNamespace
{
    public class Hurricane : Boss
    {
        protected float health = 500;
        [SerializeField] private float leftX = -7f;   // World X pos for left side
        [SerializeField] private float rightX = 7f;   // World X pos for right side
        [SerializeField] private float switchSpeed = 5f; // Speed for smooth switching
        private bool isOnLeftSide = true;
        private bool isSwitchingSides = false;

        protected override void Start()
        {
            // Hurricane spawns at the left side
            transform.position = new Vector3(leftX, transform.position.y, transform.position.z);
            base.Start();
            SwitchSides();
        }

        protected override void Update()
        {
            // The Hurricane isn't defeated when it runs out of attacks
            // Instead it is defeated when it runs out of health
            // Therefore it should loop attacks:
            if (waveSpawner.currentWaveIndex >= waveSpawner.waves.Length)
            {
                Debug.Log("Disaster " + gameObject.name + " has looped its wave attacks!");

                waveSpawner.Restart();

                TakeDamage(250); //This is just for debugging it won't actually take damage here
            }

            // Movement for switching to the other side
            if (isSwitchingSides)
            {
                float targetX = isOnLeftSide ? rightX : leftX;
                Vector3 targetPos = new Vector3(targetX, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPos, switchSpeed * Time.deltaTime);

                //Reached the other side
                if (Vector3.Distance(transform.position, targetPos) < 0.01f)
                {
                    transform.position = targetPos;
                    isOnLeftSide = !isOnLeftSide;
                    isSwitchingSides = false;
                }
            }
        }

        protected void SwitchSides()
        {
            if (isSwitchingSides) return; // Prevent spamming switch
            isSwitchingSides = true;
        }

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