using DefaultNamespace;
using UnityEngine;
using System.Collections;
    public class TidalWave : Enemy
    // Tidal Wave attack
    {   
        protected override void Start()
        {
            speed = 0.3f;
            health = 100;
            damage = 50;
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