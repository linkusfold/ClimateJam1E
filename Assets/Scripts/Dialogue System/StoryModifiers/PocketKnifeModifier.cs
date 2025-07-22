using Game_Manager;
using UnityEngine;

public class PocketKnifeModifier : MonoBehaviour
{
    public CharacterDialogue Branch1;
    public CharacterDialogue Branch2;
    public void Start()
    {

        CharacterHouse characterHouse = GetComponent<CharacterHouse>();
        int storyEventStatus = GameManager.instance.storyEvents[(int)StoryChoices.givePocketKnife];
        if (storyEventStatus == 1)
        {
            characterHouse.dialogue = Branch1;
        }
        else
        {
            characterHouse.dialogue = Branch2;
        }
    }
}
