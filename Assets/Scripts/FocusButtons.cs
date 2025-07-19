using System.Collections.Generic;
using UnityEngine;

public class FocusButtons : MonoBehaviour
{
    public List<FocusButton> btns;

    public void Focus(FocusButton newFocusedBtn)
    {
        FocusButton currentBtn = btns.Find(button => button.towerButton.focused);
        if(currentBtn) currentBtn.towerButton.focused = false;
        newFocusedBtn.towerButton.focused = true;
        foreach (FocusButton btn in btns) btn.UpdateSprite();
    }
}