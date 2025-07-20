using DefaultNamespace;
using UnityEngine;
    public class Crab : Enemy
    // Oilgae minion; has melee claw attack
    {   
        protected override void Start()
        {
            speed = 0.8f;
            health = 110;
            damage = 7;
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