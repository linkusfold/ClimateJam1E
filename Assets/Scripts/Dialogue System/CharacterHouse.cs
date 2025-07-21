using Unity.VisualScripting;
using UnityEngine;
using Game_Manager;
[RequireComponent(typeof(CharacterDialogueHandler))]
public class CharacterHouse : MonoBehaviour
{
    
    CharacterDialogueHandler characterDialogueHandler;
    public CharacterDialogue dialogue;
    public CharacterDialogue convoOverDialogue;
    public CharacterDialogue houseDestroyedDialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterDialogueHandler = GetComponent<CharacterDialogueHandler>();
        // characterDialogueHandler.StopDialogue();
        characterDialogueHandler.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnMouseDown()
    {
        // Close previous dialogue
        Debug.LogWarning("Clicked!");
        CharacterHouse currentDialogue = this;
        if (currentDialogue != null)
        {
            Debug.Log("Found the current dialogue house");
            currentDialogue.StopDialogueHandler();
            currentDialogue.characterDialogueHandler.SkipTextLoading();
        }

        StartDialogueHandler();
    }
    public void StartDialogueHandler()
    {
        
        characterDialogueHandler.enabled = true;
        characterDialogueHandler.currentDialogue = dialogue;
        characterDialogueHandler.StartDialogue();
    }
    public void StopDialogueHandler()
    {
        Debug.Log("Stopping!");
        characterDialogueHandler.SkipTextLoading();
        characterDialogueHandler.enabled = false;
    }
}
