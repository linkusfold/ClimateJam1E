using UnityEngine;

namespace DefaultNamespace
{
    public class Jellyfish : Enemy
    // Jellyfish minion; has ranged tentacle attack
    {
        protected override void Start()
        {
            speed = 0.4f;
            health = 100;
            damage = 8;
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
        {
            building.TakeDamage((int)damage);
            //Debug.Log("Jellyfish attacks with tentacle!");
            // Custom logic here for the jellyfish ranged attack
        }

    }
}