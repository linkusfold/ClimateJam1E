using DefaultNamespace;
using UnityEngine;
    public class Rock : Enemy
    // Falling Rock attack
    {   
        protected override void Start()
        {
            speed = 0.8f;
            health = 150;
            damage = 40;
            atkSpeed = 50; //only attacks once
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
        {
            building.TakeDamage((int)damage);
            //Debug.Log($"Rock hits the {building} for {damage} points of damage!");
            Die();
        }

    }