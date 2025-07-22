using UnityEngine;
using System.Collections;

namespace DefaultNamespace
{
    public class Volcano : Boss
    {
        [SerializeField] private float leftX = -8f;   // World X pos for left side
        [SerializeField] private float rightX = 8f;   // World X pos for right side
        [SerializeField] private float switchDuration = 10f;
        [SerializeField] private float offscreenY = -10f; // How far down the boss goes to disapear
        private bool isOnLeftSide = false;
        private bool isSwitchingSides = false;

        protected override void Start()
        {
            // Hurricane spawns at the left side
            transform.position = new Vector3(rightX, transform.position.y, transform.position.z);
            base.Start();
        }

        protected override void Update()
        {
            base.Update();

            if (((waveSpawner.levelData.currentWaveIndex % 4 >= 2 && !isOnLeftSide)
                || (waveSpawner.levelData.currentWaveIndex % 4 < 2 && isOnLeftSide))
                && !isSwitchingSides)
            {
                SwitchSides();
            }
        }

        private IEnumerator SwitchSidesRoutine()
        {
            //Co-routine for the Volcano to switch sides by going down into the water on
            //one side and re-emerging on the other
            isSwitchingSides = true;

            float startX = isOnLeftSide ? leftX : rightX;
            float endX = isOnLeftSide ? rightX : leftX;

            Vector3 diveStartPos = new Vector3(startX, transform.position.y, transform.position.z);
            Vector3 diveEndPos = new Vector3(startX, offscreenY, transform.position.z); // Goes offscreen vertically

            float diveTime = switchDuration / 2f;
            float riseTime = switchDuration / 2f;

            // Dive phase
            float timeElapsed = 0f;
            while (timeElapsed < diveTime)
            {
                float t = timeElapsed / diveTime;
                float y = Mathf.Lerp(diveStartPos.y, diveEndPos.y, t);
                transform.position = new Vector3(startX, y, transform.position.z);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Flip sprite
            Vector3 localScale = transform.localScale;
            localScale.x = -localScale.x;
            transform.localScale = localScale;

            // Teleport to other side offscreen
            Vector3 riseStartPos = new Vector3(endX, offscreenY, transform.position.z);
            Vector3 riseEndPos = new Vector3(endX, diveStartPos.y, transform.position.z);

            transform.position = riseStartPos;

            // Rise phase
            timeElapsed = 0f;
            while (timeElapsed < riseTime)
            {
                float t = timeElapsed / riseTime;
                float y = Mathf.Lerp(riseStartPos.y, riseEndPos.y, t);
                transform.position = new Vector3(endX, y, transform.position.z);
                timeElapsed += Time.deltaTime;
                yield return null;
            }

            // Snap to final position
            transform.position = riseEndPos;

            isOnLeftSide = !isOnLeftSide;
            isSwitchingSides = false;
            waveSpawner.FlipPathing();
        }

        protected void SwitchSides()
        {
            if (!isSwitchingSides)
            {
                StartCoroutine(SwitchSidesRoutine());    
            }
        }
    }
}