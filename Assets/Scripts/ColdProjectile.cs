using DefaultNamespace;
using UnityEngine;

/*
 * -----------------------------------------------
 * ColdProjectile.cs
 * Author: Angel
 * Date: July 2, 2025
 *
 * A cold-based projectile that deals damage and also
 * extinguishes fire effects on enemies it hits.
 * -----------------------------------------------
 */

public class ColdProjectile : MonoBehaviour
{
    public float speed = 8f;         // Speed at which the projectile travels
    public float damage = 1f;           // Damage dealt on impact

    private Transform target;        // The enemy this projectile is pursuing

    // Assigns the target enemy when fired by a tower
    public void SetTarget(Transform t)
    {
        target = t;
    }

    void Update()
    {
        // Destroy the projectile if the target no longer exists
        if (target is null)
        {
            Destroy(gameObject);
            return;
        }

        // Move the projectile toward the target
        Vector3 dir = (target.position - transform.position).normalized;
        transform.position += dir * (speed * Time.deltaTime);
    }

    // Handles collision with enemy targets
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            // Try to get any damageable component
            var damageable = other.GetComponent<IDamageableEnemy>();
            if (damageable != null)
            {
                damageable.TakeDamage(damage); // Apply damage
            }

            // Try to extinguish fire only if it's a normal enemy
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ExtinguishFire(); // Remove fire status if applicable
            }

            Destroy(gameObject); // Destroy the projectile after hit
        }
    }
}