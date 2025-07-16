using System;
using Game_Manager;
using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    private Button btn;
    
    public GameObject towerPrefab;
    [NonSerialized] public Tower tower;
    [NonSerialized] public bool onCooldown = false;
    private float cooldownTimer = 0f;
    public float cooldown = 10f;

    private void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnClick);
    }

    private void Update()
    {
        if(!onCooldown) return;
        
        cooldownTimer -= Time.deltaTime;
        if(btn) btn.image.fillAmount = (cooldown-cooldownTimer) / cooldown;

        // If ready to fire again
        if (cooldownTimer <= 0f)
        {
            onCooldown = false;
        }
    }
    
    private void OnClick()
    {
        if(onCooldown) return;
        
        if(tower) GameManager.instance.RemoveTower(tower);
        GameManager.instance.btn = this;
        GameManager.instance.SelectTower(towerPrefab.GetComponent<Tower>());
        cooldownTimer = cooldown;
        onCooldown = true;
    }
}
