using UnityEngine;

namespace DefaultNamespace
{
    public class Jellyfish : Enemy
    // Jellyfish minion; has ranged tentacle attack
    {
        protected override void Start()
        {
            speed = 0.6f;
            health = 80;
            damage = 6;
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