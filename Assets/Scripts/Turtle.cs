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
            defense = 20;
            damage = 10;
            base.Start();
        }

        protected override void Attack()
        {
            Debug.Log("Turtle bites!");
            // Custom logic here for the turtle melee attack
        }

    }
}