using UnityEngine;

namespace DefaultNamespace
{
    public class Jellyfish : PathingEnemy
    // Jellyfish minion; has ranged tentacle attack
    {
        protected override void Start()
        {
            speed = 0.8f;
            health = 80;
            damage = 10;
            base.Start();
        }

        protected override void Attack()
        {
            Debug.Log("Jellyfish attacks with tentacle!");
            // Custom logic here for the jellyfish ranged attack
        }

    }
}