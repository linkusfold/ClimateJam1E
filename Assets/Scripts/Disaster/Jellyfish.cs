using UnityEngine;

namespace DefaultNamespace
{
    public class Jellyfish : Enemy
    // Jellyfish minion; has ranged tentacle attack
    {
        protected override void Start()
        {
            speed = 1.2f;
            health = 80;
            damage = 10;
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
        {
            Debug.Log("Jellyfish attacks with tentacle!");
            // Custom logic here for the jellyfish ranged attack
        }

    }
}