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
        characterDialogueHandler.StopDialogue();
        characterDialogueHandler.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnMouseDown()
    {
        StartDialogueHandler();
    }
    public void StartDialogueHandler()
    {
        characterDialogueHandler.enabled = true;
        characterDialogueHandler.StartDialogue();
    }
    public void StopDialogueHandler()
    {
        characterDialogueHandler.enabled = false;
    }
}
