using UnityEngine;

namespace DefaultNamespace
{
    public class Crab : Enemy
    // Oilgae minion; has melee claw attack
    {   
        protected override void Start()
        {
            speed = 1.5f;
            health = 110;
            damage = 10;
            base.Start();
        }

        protected override void Attack()
        {
            Debug.Log("Crab claws!");
            // Custom logic here for the crab melee attack
        }

    }
}