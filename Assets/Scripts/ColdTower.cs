using UnityEngine;

/*
 * -----------------------------------------------
 * ColdTower.cs
 * Author: Angel
 * Date: July 2, 2025
 *
 * Specialized tower that shoots cold-based projectiles
 * to apply freezing or slowing effects to enemies.
 * Overrides the Shoot method from Tower.
 * -----------------------------------------------
 */

public class ColdTower : Tower
{
    // Override the base Shoot method to fire a ColdProjectile
    protected override void Shoot(Transform enemy)
    {
        // Instantiate the projectile at the tower's position
        GameObject water = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Assign the enemy target so the projectile can chase it
        water.GetComponent<ColdProjectile>().SetTarget(enemy);
    }
}