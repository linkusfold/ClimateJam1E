using DefaultNamespace;
using UnityEngine;
    public class Crab : Enemy
    // Oilgae minion; has melee claw attack
    {   
        protected override void Start()
        {
            speed = 0.6f;
            health = 100;
            damage = 4;
            atkSpeed = 1;
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
        {
            building.TakeDamage((int)damage);
            //Debug.Log($"Crab clawed the {building} for {damage} points of damage!");
            // Custom logic here for the crab melee attack
        }

    }