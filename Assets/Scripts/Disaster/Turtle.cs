using UnityEngine;

namespace DefaultNamespace
{
    public class Turtle : Enemy
    // Turtle minion; has ranged tentacle attack
    {
        protected override void Start()
        {
            speed = 0.5f;
            health = 150;
            damage = 10;
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
        {
            building.TakeDamage((int)damage);
            Debug.Log("Turtle bites!");
            // Custom logic here for the turtle melee attack
        }

    }
}