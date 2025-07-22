using System;
using DefaultNamespace;
using Game_Manager;
using UnityEngine;

public class House : MonoBehaviour, IHealable, IDamageableBuilding
{
    public bool isDestroyed = false;             // Indicates if the tower has been destroyed
    public bool IsDestroyed { set => isDestroyed = value; get => isDestroyed; }
        
    public float health = 100f;
    [NonSerialized] public float maxHealth;
    protected TowerHealth healthBar;
        
    private void Awake()
    {
        maxHealth = health;
        healthBar = transform.GetComponentInChildren<TowerHealth>();
    }

    private void Start()
    {
        GameManager.instance.houses.Add(this);
    }
        
    private void Destroy()
    {
        isDestroyed = true;
        Debug.Log("House destroyed");
        if (!GameManager.instance.CheckHousesAlive())
        {
            GameManager.instance.Lose();
        }
    }
        
    public void UpdateHealthBar()
    {
        if(healthBar) healthBar.UpdateHealthBar(health, maxHealth);;
    }

    public void TakeDamage(int damage)
    {
        if (isDestroyed) return;
            
        health = Mathf.Max(0, health - damage);
        UpdateHealthBar();
        if(health <= 0) Destroy();
    }

    public void Heal(int amount)
    {
        if (isDestroyed) return;
            
        health = Mathf.Min(maxHealth, health + amount);
        UpdateHealthBar();
    }
}