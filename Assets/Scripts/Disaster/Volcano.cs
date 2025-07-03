using UnityEngine;

namespace DefaultNamespace
{
    public class Volcano : Boss
    {
        [SerializeField] private float leftX = -7f;   // World X pos for left side
        [SerializeField] private float rightX = 7f;   // World X pos for right side
        [SerializeField] private float switchSpeed = 5f;
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
            base.Update();

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
            if (!isSwitchingSides)
            {
                isSwitchingSides = true;
            }
        }
    }
}