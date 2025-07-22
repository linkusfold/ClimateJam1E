using DefaultNamespace;
using UnityEngine;
using System.Collections;
    public class Ash : Enemy
    // Falling ash attack
    {   
        protected override void Start()
        {
            speed = 0.5f;
            health = 70;
            damage = 2;
            atkSpeed = 2; //only attacks once
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
    {
        building.TakeDamage((int)damage);
        //Die();
    }

    }