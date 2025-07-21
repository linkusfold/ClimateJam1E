using DefaultNamespace;
using UnityEngine;
using System.Collections;
    public class Rock : Enemy
    // Falling Rock attack
    {   
        protected override void Start()
        {
            speed = 0.6f;
            health = 80;
            damage = 40;
            atkSpeed = 40; //only attacks once
            currentNodeId = 6; //start on the actual cliff
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