using DefaultNamespace;
using UnityEngine;
using System.Collections;
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

            StartCoroutine(DelayedFall());
        }

        private IEnumerator DelayedFall()
        {
            pathing = false;
            yield return new WaitForSeconds(3f);

            pathing = true;
        }

        protected override void PerformAttack(IDamageableBuilding building)
    {
        building.TakeDamage((int)damage);
        //Debug.Log($"Rock hits the {building} for {damage} points of damage!");
        Die();
    }

    }