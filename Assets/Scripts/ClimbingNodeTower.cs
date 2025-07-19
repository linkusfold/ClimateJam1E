using DefaultNamespace;
using UnityEngine;

/*
 * -----------------------------------------------
 * ClimbingNodeTower.cs
 * Author: Angel
 * Date: July 2, 2025
 *
 * Specialized tower that passively heals nearby objects that implement IHealable.
 * Healing is done in a radius every physics tick.
 * -----------------------------------------------
 */

public class ClimbingNodeTower : Tower
{
    public float healRadius = 3f;    // Radius within which this tower can heal
    public int healAmount = 1;       // How much health to restore per heal tick
    
    void Update()
    {
        // Skip update if the tower hasn't been unlocked yet
        if (isDestroyed) return;

        if (health <= 0)
        {
            Destroy();
            return;
        }
        
        if(!isUnlocked) return;

        // Decrease the cooldown timer over time
        cooldownTimer -= Time.deltaTime;
        //if(btn) btn.image.fillAmount = (fireCooldown-cooldownTimer) / fireCooldown;

        // If ready to fire again
        if (cooldownTimer <= 0f)
        {
            Shoot(new Enemy());              // Fire at the enemy
            cooldownTimer = fireCooldown;        // Reset cooldown timer
        }
    }


    protected override void Shoot(Transform enemy)
    {
        // Skip healing if the tower is not yet unlocked
        if (!isUnlocked) return;

        // Get all colliders within the healing radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, healRadius, LayerMask.GetMask("Tower"));

        foreach (var hit in hits)
        {
            // Check if the object has a healable component
            IHealable healable = hit.GetComponent<IHealable>();
            if (healable != null)
            {
                // Apply healing
                healable.Heal(healAmount);
            }
        }
    }
}