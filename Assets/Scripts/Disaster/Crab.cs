using DefaultNamespace;
using UnityEngine;
    public class Crab : Enemy
    // Oilgae minion; has melee claw attack
    {   
        protected override void Start()
        {
            speed = 0.5f;
            health = 50;
            damage = 2;
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