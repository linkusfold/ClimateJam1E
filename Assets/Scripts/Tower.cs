using System;
using DefaultNamespace;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/*
 * -----------------------------------------------
 * Tower.cs
 * Author: Angel
 * Date: July 2, 2025
 *
 * Base class for all towers. Handles enemy detection,
 * firing cooldowns, and upgrade logic.
 * Specialized towers should extend this class.
 * -----------------------------------------------
 */

public class Tower : MonoBehaviour, IPointerClickHandler, IHealable, IDamageableBuilding
{
    public int level = 1;                         // Current level of the tower
    public bool isUnlocked = false;              // Indicates if the tower is active and allowed to shoot
    public bool isDestroyed = false;             // Indicates if the tower has been destroyed
    public bool IsDestroyed { set => isDestroyed = value; get => isDestroyed; }
    public GameObject projectilePrefab;          // Prefab to instantiate when the tower fires
    public float attackRange = 5f;               // Maximum range within which enemies can be targeted
    public float fireCooldown = 1.5f;            // Time delay (in seconds) between consecutive shots
    protected float cooldownTimer = 0f;            // Tracks cooldown progress internally
    protected Button btn;
    protected TMP_Text levelText;
    
    public float health = 100f;
    [NonSerialized] public float maxHealth;
    protected TowerHealth healthBar;
    
    [Header("Audio")]
    public AudioClip shootSound;
    
    public enum DisasterType { Oilgae, Volcano, Hurricane, TheShip }

    private void Awake()
    {
        levelText = transform.GetComponentInChildren<TMP_Text>();
        levelText.text = level.ToString();
        
        maxHealth = health;
        healthBar = transform.GetComponentInChildren<TowerHealth>();
    }

    void Update()
    {
        // Skip update if the tower hasn't been unlocked yet
        if (isDestroyed) return;

        if (health <= 0)
        {
            DestroyTower();
            return;
        }
        
        if(!isUnlocked) return;

        // Decrease the cooldown timer over time
        cooldownTimer -= Time.deltaTime;
        //if(btn) btn.image.fillAmount = (fireCooldown-cooldownTimer) / fireCooldown;

        // If ready to fire again
        if (cooldownTimer <= 0f)
        {
            GameObject enemy = FindNearestEnemy();   // Look for closest enemy in range
            if (enemy is not null)
            {
                Shoot(enemy.transform);              // Fire at the enemy
                cooldownTimer = fireCooldown;        // Reset cooldown timer
            }
        }
    }

    // Searches for the nearest enemy within attack range
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); // Get all enemies in scene
        float minDist = Mathf.Infinity;     // Start with a very large distance
        GameObject closest = null;

        foreach (GameObject e in enemies)
        {
            float dist = Vector3.Distance(transform.position, e.transform.position);
            if (dist < minDist && dist <= attackRange)
            {
                minDist = dist;
                closest = e;
            }
        }

        return closest;
    }

    // Fires a projectile toward the given enemy
    // Meant to be overridden by specialized tower subclasses
    protected virtual void Shoot(Transform enemy)
    {
        MakeShootSound();
        GameObject proj = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        proj.GetComponent<Projectile>().SetTarget(enemy);  // Assign the enemy as the projectile’s target
    }
    
    protected void MakeShootSound()
    {
        if (!shootSound) return;
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    public virtual void OnClick(Button btn)
    {
        this.btn = btn;
        Debug.Log("Tower button clicked");
    }

    
    public void Upgrade()
    {
        level++;                // Increase tower level
        isUnlocked = true;      // Ensure the tower is now active
        levelText.text = level.ToString();
        Debug.Log($"Tower upgraded to level {level}");
    }

    protected void DestroyTower()
    {
        isDestroyed = true;
        Debug.Log("Tower destroyed");
    }

    public void UpdateHealthBar()
    {
        if(healthBar) healthBar.UpdateHealthBar(health, maxHealth);;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log($"Tower upgraded to level {level}");
        RadialMenuManager.Instance.ShowMenu(this);
    }

    public void Heal(int amount)
    {
        health += amount;
        UpdateHealthBar();
    }

    public void TakeDamage(int damage)
    {
        health = Mathf.Max(0, health - damage);
        UpdateHealthBar();
    }
}
