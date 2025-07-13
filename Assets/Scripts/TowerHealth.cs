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
        if (healthBarForeground != null)
        {
            float ratio = currentHealth / maxHealth;
            healthBarForeground.localScale = new Vector3(ratio * originalScale.x, originalScale.y, originalScale.z);
        }
    }
}