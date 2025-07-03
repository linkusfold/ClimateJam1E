using DefaultNamespace;
using UnityEngine;

/*
 * -----------------------------------------------
 * Projectile.cs
 * Author: Angel
 * Date: July 2, 2025
 *
 * Controls projectile movement and collision logic.
 * The projectile moves toward a designated enemy and
 * deals damage on impact, then destroys itself.
 * -----------------------------------------------
 */

public class Projectile : MonoBehaviour
{
    public float speed = 10f;     // Movement speed of the projectile
    public int damage = 1;        // Damage dealt to the enemy on hit

    private Transform target;     // The enemy this projectile is chasing

    // Called by the tower to assign an enemy to target
    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        // If the target no longer exists, destroy the projectile
        if (target is null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile toward the target enemy
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * (speed * Time.deltaTime);
        // Rotates the projectile
        transform.up = dir;
    }

    // Triggered when the projectile hits something with a collider
    void OnTriggerEnter2D(Collider2D other)
    {
        // Only react if the object hit is tagged "Enemy"
        if (other.CompareTag("Enemy"))
        {
            // Apply damage to the enemy
            other.GetComponent<Enemy>().TakeDamage(damage);

            // Destroy the projectile after impact
            Destroy(gameObject);
        }
    }
}