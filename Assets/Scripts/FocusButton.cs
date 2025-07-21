using UnityEngine;
using UnityEngine.UI;

public class FocusButton : MonoBehaviour
{
    private Sprite _unfocusedSprite;
    public Sprite focusedSprite;
    
    public TowerButton towerButton;
    private FocusButtons _btnParent;

    void Awake()
    {
        if (!towerButton)
        {
            Debug.LogError($"{gameObject.name}: FocusButton has no towerButton.");
            return;
        }
        gameObject.GetComponent<Button>().onClick.AddListener(OnClick);
        _btnParent = transform.GetComponentInParent<FocusButtons>();
        if (!_btnParent)
        {
            Debug.LogError($"{gameObject.name}: FocusButton has no FocusButtons parent.");
            return;
        }
        
        _unfocusedSprite = gameObject.GetComponent<Image>().sprite;
        _btnParent.btns.Add(this);
    }
    void OnClick()
    {
        if (!_btnParent && !towerButton )
        {
            Debug.LogError($"{gameObject.name}: Broken button, see earlier errors.");
            return;
        }
        if (towerButton.focused)
        {
            towerButton.focused = false;
            UpdateSprite();
            return;
        }

        _btnParent.Focus(this);
    }

    public void UpdateSprite()
    {
        if (towerButton.focused)
        {
            gameObject.GetComponent<Image>().sprite = focusedSprite;
            return;
        }
        gameObject.GetComponent<Image>().sprite = _unfocusedSprite;
    }
}