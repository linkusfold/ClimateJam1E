using DefaultNamespace;
using UnityEngine;
    public class Fog : Enemy
    // Tutorial Enemy
    {   
        protected override void Start()
        {
            speed = 0.5f;
            health = 20;
            damage = 0;
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