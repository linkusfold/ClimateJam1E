using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class HealthBar : MonoBehaviour
    {
        public Slider healthSlider;
        private float _maxHealth = 100f;
        public float MaxHealth
        {
            get => _maxHealth;
            set
            {
                _maxHealth = value;
                healthSlider.maxValue = value;
                Health = value;
                healthSlider.value = Health;
            }
        }
        private float _health;
        public float Health
        {
            get => _health;
            set
            {
                _health = Mathf.Clamp(value, 0, MaxHealth);
            }
        }

        private void Update()
        {
            //Currently I directly snap to the new health value
            //We can switch this to smoothly transition later
            if (healthSlider.value != Health)
            {
                healthSlider.value = Health;
            }
        }
    }
}