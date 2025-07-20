using DefaultNamespace;
using UnityEngine;
using System.Collections;
    public class Rock : Enemy
    // Falling Rock attack
    {   
        protected override void Start()
        {
            speed = 0.6f;
            health = 150;
            damage = 60;
            atkSpeed = 50; //only attacks once
            base.Start();

            Immobilize(3f);
        }

        protected override void PerformAttack(IDamageableBuilding building)
    {
        building.TakeDamage((int)damage);
        //Debug.Log($"Rock hits the {building} for {damage} points of damage!");
        Die();
    }

    }