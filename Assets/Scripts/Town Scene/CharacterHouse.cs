using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterDialogueHandler))]
public class CharacterHouse : MonoBehaviour
{
    
    CharacterDialogueHandler characterDialogueHandler;

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
        CharacterHouse currentDialogue = GameManager.instance.currentDialogue;
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
        GameManager.instance.currentDialogue = this;
        characterDialogueHandler.enabled = true;
        characterDialogueHandler.StartDialogue();
    }
    public void StopDialogueHandler()
    {
        Debug.Log("Stopping!");
        characterDialogueHandler.SkipTextLoading();
        characterDialogueHandler.enabled = false;
    }
}
