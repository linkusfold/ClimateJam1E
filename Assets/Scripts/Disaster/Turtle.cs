using UnityEngine;

namespace DefaultNamespace
{
    public class Turtle : Enemy
    // Turtle minion; has ranged tentacle attack
    {
        protected override void Start()
        {
            speed = 0.2f;
            health = 100;
            damage = 6;
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
        {
            building.TakeDamage((int)damage);
            //Debug.Log("Turtle bites!");
            // Custom logic here for the turtle melee attack
        }

    }
}