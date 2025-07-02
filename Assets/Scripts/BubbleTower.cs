using UnityEngine;

/*
 * -----------------------------------------------
 * BubbleTower.cs
 * Author: Angel
 * Date: July 2, 2025
 *
 * A specialized type of Tower that shoots bubbles
 * instead of standard projectiles. Overrides the
 * base Shoot method to use a BubbleProjectile.
 * -----------------------------------------------
 */

public class BubbleTower : Tower
{
    // Overrides the base Tower's Shoot behavior to use a BubbleProjectile
    protected override void Shoot(Transform enemy)
    {
        // Instantiate the bubble projectile at the tower's position
        GameObject bubble = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set the target on the bubble so it knows which enemy to chase
        bubble.GetComponent<BubbleProjectile>().SetTarget(enemy);
    }
}