using UnityEngine;
namespace DefaultNamespace
{
    public class Ship : Boss
    {
        [SerializeField] private float growSpeed = 0.2f;      // How fast it grows per second
        [SerializeField] private float moveSpeed = 1f;        // How fast it moves toward the end position
        [SerializeField] private float maxScale = 1.5f;         // Max scale limit
        [SerializeField] private Vector3 startPosition;       // Starting world position
        [SerializeField] private Vector3 endPosition;         // Target world position (e.g., near the camera)
        [SerializeField] private bool startMoving = true; 

        private Vector3 initialScale;

        protected override void Start()
        {
            transform.position = startPosition;
            initialScale = transform.localScale;
        }

        protected override void Update()
        {
            if (!startMoving) return;

            // Move toward end position
            transform.position = Vector3.MoveTowards(transform.position, endPosition, moveSpeed * Time.deltaTime);

            // Grow over time
            if (transform.localScale.x < maxScale)
            {
                float growthAmount = growSpeed * Time.deltaTime;
                transform.localScale += new Vector3(growthAmount, growthAmount, 0);
            }
            else
            {
                transform.localScale = new Vector3(maxScale, maxScale, transform.localScale.z);
            }

            // Optional: Stop once at destination and max size
            if (transform.position == endPosition && transform.localScale.x >= maxScale)
            {
                startMoving = false;
            }
        }

        // Public method to trigger it manually
        public void BeginApproach()
        {
            startMoving = true;
        }
    }
}