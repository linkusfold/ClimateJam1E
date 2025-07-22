using Game_Manager;
using UnityEngine;

public class ArchiePathModifier : MonoBehaviour
{
    public CharacterDialogue branch1;
    public CharacterDialogue branch2;

    public void Start()
    {
        CharacterHouse characterHouse = GetComponent<CharacterHouse>();

        if (GameManager.instance.storyEvents[(int)StoryChoices.archiePath] == 1)
        {
            characterHouse.dialogue = branch1;
        }
        else
        {
            characterHouse.dialogue = branch2;
        }
    }
}
