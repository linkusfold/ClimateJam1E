using UnityEngine;

public class WallTower : Tower
{

    private void Start()
    {
        isUnlocked = true; // Walls should work immediately when placed
        levelText.text = "0"; // Or whatever you want to show on the wall UI
    }

    protected override void Shoot(Transform enemy)
    {
        // Do nothing — wall doesn't shoot
    }

    public void TakeDisasterDamage(float amount, DisasterType type)
    {
        switch (type)
        {
            case DisasterType.Oilgae:
            case DisasterType.Volcano:
            case DisasterType.Hurricane:
            case DisasterType.TheShip:
                ApplyDamage(amount);
                break;
            default:
                Debug.LogWarning("Unknown disaster type!");
                break;
        }
    }

    private void ApplyDamage(float amount)
    {
        health -= amount;
        healthBar.UpdateHealthBar(health, maxHealth);
        
        if (health <= 0)
        {
            Destroy(gameObject); // Wall is destroyed
        }
    }

    public float GetRemainingHealth()
    {
        return health;
    }
}