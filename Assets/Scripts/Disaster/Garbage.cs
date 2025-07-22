using DefaultNamespace;
using UnityEngine;
    public class Garbage : Enemy
    {   
        protected override void Start()
        {
            speed = 0.6f;
            health = 170;
            damage = 20;
            atkSpeed = 0.1f;
            base.Start();
        }

        protected override void PerformAttack(IDamageableBuilding building)
        {
            building.TakeDamage((int)damage);
        }

    }