using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using Game_Manager;

[RequireComponent(typeof(CharacterDialogueHandler))]
public class CharacterHouse : MonoBehaviour
{

    CharacterDialogueHandler characterDialogueHandler;
    public CharacterDialogue dialogue;
    public CharacterDialogue convoOverDialogue;
    public CharacterDialogue houseDestroyedDialogue;
    public Homes characterHouseType;
    public UnityEvent m_event;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterDialogueHandler = GetComponent<CharacterDialogueHandler>();
        // characterDialogueHandler.StopDialogue();
        characterDialogueHandler.enabled = false;
        if (GameManager.instance.isHouseDestroyed(characterHouseType))
        {
            dialogue = houseDestroyedDialogue;
            convoOverDialogue = houseDestroyedDialogue;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnMouseDown()
    {
        // Close previous dialogue
        Debug.LogWarning("Clicked!");
        if (GameManager.instance.inConversation)
        {
            return;
        }

        CharacterHouse currentDialogueHouse = this;
        if (currentDialogueHouse != null)
        {
            Debug.Log("Found the current dialogue house");
            currentDialogueHouse.StopDialogueHandler();
            currentDialogueHouse.characterDialogueHandler.SkipTextLoading();
        }

        StartDialogueHandler();
    }
    public void StartDialogueHandler()
    {
        GameManager.instance.inConversation = true;
        characterDialogueHandler.enabled = true;
        characterDialogueHandler.currentDialogue = dialogue;
        characterDialogueHandler.StartDialogue();
    }
    public void StopDialogueHandler()
    {
        Debug.Log("Stopping!");
        characterDialogueHandler.SkipTextLoading();
        characterDialogueHandler.StopDialogue();
        characterDialogueHandler.enabled = false;
        
    }

    public void EndConversation()
    {
        GameManager.instance.inConversation = false;
        dialogue = convoOverDialogue;
        StopDialogueHandler();
        DoneTalking.HandleScene();
    }
    
    
}
