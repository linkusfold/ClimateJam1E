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
    public int damage = 1;           // Damage dealt on impact

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
            Enemy e = other.GetComponent<Enemy>();
            if (e != null)
            {
                e.TakeDamage(damage);      // Apply damage
                e.ExtinguishFire();        // Remove fire status if applicable
            }

            Destroy(gameObject);           // Destroy the projectile on impact
        }
    }
}