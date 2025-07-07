using UnityEngine;

/*
 * -----------------------------------------------
 * BubbleProjectile.cs
 * Author: Angel
 * Date: July 2, 2025
 *
 * Controls the behavior of bubble projectiles.
 * These projectiles move toward a target enemy and
 * apply an immobilization effect on impact.
 * -----------------------------------------------
 */

public class BubbleProjectile : MonoBehaviour
{
    public float speed = 7f;                      // Movement speed of the bubble
    public float immobilizeDuration = 5f;         // How long the enemy is frozen after getting hit
    private Transform target;                     // The enemy this bubble is chasing

    // Assigns the enemy target when fired
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

        // Move toward the target enemy
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * (speed * Time.deltaTime);
    }

    // When the bubble collides with an enemy, apply the immobilize effect and destroy itself
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy is not null)
            {
                // Freeze the enemy for a set duration
                enemy.Immobilize(immobilizeDuration);
            }

            // Destroy the bubble after it hits
            Destroy(gameObject);
        }
    }
}