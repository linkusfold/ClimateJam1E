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

    void FixedUpdate()
    {
        // Skip healing if the tower is not yet unlocked
        if (!isUnlocked) return;

        // Get all colliders within the healing radius
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, healRadius);

        foreach (var hit in hits)
        {
            // Only heal objects tagged as "House" or "Tower"
            if (hit.CompareTag("House") || hit.CompareTag("Tower"))
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
}