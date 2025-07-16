using Unity.Mathematics.Geometry;
using UnityEngine;

public class TowerHealth : MonoBehaviour
{
    public Transform healthBarForeground;

    private Vector3 originalScale;

    void Awake()
    {
        if (healthBarForeground != null)
            originalScale = healthBarForeground.localScale;
    }

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthBarForeground == null) return;
        
        float ratio = Mathf.Max(0, Mathf.Min(maxHealth, currentHealth)) / maxHealth;
        healthBarForeground.localScale = new Vector3(ratio * originalScale.x, originalScale.y, originalScale.z);
    }
}