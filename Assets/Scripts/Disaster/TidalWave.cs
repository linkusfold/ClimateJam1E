using DefaultNamespace;
using UnityEngine;
using System.Collections;
public class TidalWave : Enemy
// Tidal Wave attack
{
    float attackRange = 2f;
    protected override void Start()
    {
        speed = 0.3f;
        health = 260;
        damage = 70;
        atkSpeed = 50; //only attacks once
        base.Start();
    }

    protected override void PerformAttack(IDamageableBuilding building)
    {
        BigWaveAttack();

        //Destroy the wave
        Die();
    }

    private void BigWaveAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, attackRange, LayerMask.GetMask("Tower"));

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out IDamageableBuilding bldg) && !bldg.IsDestroyed)
            {
                bldg.TakeDamage((int)damage);
            }
        }
    }

    protected override void OnReachedEnd()
    {
        BigWaveAttack();
        base.OnReachedEnd();
    }
    

    }