using DefaultNamespace;
using UnityEngine;
using System.Collections;
    public class Lightning : Enemy
    // Lightning bolt attack
    {   
        protected override void Start()
        {
            speed = 0.8f;
            health = 80;
            damage = 25;
            atkSpeed = 50; //only attacks once
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
    {
        building.TakeDamage((int)damage);
        Die();
    }

    }