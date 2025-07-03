using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{
    public class Hurricane : Boss
    {
        [SerializeField] private HealthBar healthBar;

        [SerializeField] private float _health = 500f;

        public float Health
        {
            get => _health;
            //when you set the health, this automatically updates the health in HealthBar
            set
            {
                _health = value;
                healthBar.Health = value;
            }
        }
        
        [SerializeField] private float leftX = -7f;   // World X pos for left side
        [SerializeField] private float rightX = 7f;   // World X pos for right side
        [SerializeField] private float switchDuration = 2f;
        [SerializeField] private float waveHeight = 2f; //movement wave height for switching sides
        [SerializeField] private int waveFrequency = 2; //movement wave frequency for switching sides
        private bool isOnLeftSide = true;
        private bool isSwitchingSides = false;

        protected override void Start()
        {
            // Hurricane spawns at the left side
            transform.position = new Vector3(leftX, transform.position.y, transform.position.z);

            healthBar.MaxHealth = Health;

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
                SwitchSides();
            }
        }

        private IEnumerator SwitchSidesRoutine()
        {
            //Co-routine for the Hurriacne to switch sides in a wave pattern
            isSwitchingSides = true;

            float startX = isOnLeftSide ? leftX : rightX;
            float endX = isOnLeftSide ? rightX : leftX;
            Vector3 startPos = new Vector3(startX, transform.position.y, transform.position.z);
            Vector3 endPos = new Vector3(endX, transform.position.y, transform.position.z);

            float timeElapsed = 0f;

            // Flip the sprite
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x; // Flip X scale
            transform.localScale = localScale;

            while (timeElapsed < switchDuration)
            {
                float t = timeElapsed / switchDuration;
                float sineOffset = Mathf.Sin(t * Mathf.PI * waveFrequency) * waveHeight;

                float x = Mathf.Lerp(startX, endX, t);
                float y = Mathf.Lerp(startPos.y, endPos.y, t) + sineOffset;

                transform.position = new Vector3(x, y, transform.position.z);

                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Snap to final position
            transform.position = endPos;

            isOnLeftSide = !isOnLeftSide;
            isSwitchingSides = false;
        }

        protected void SwitchSides()
        {
            if (!isSwitchingSides)
            {
                StartCoroutine(SwitchSidesRoutine());    
            }
        }

        public void TakeDamage(float amount)
        {
            Health -= amount;

            Debug.Log("Disaster " + gameObject.name + " health reduced to " + Health + "!");

            if (Health <= 0)
            {
                Die();
            }
        }
    }
}