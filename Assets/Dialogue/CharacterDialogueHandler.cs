using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CharacterDialogueHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    CharacterDialogue currentDialogue;
    public int currentPassageIndex;
    String currentText;
    public TMP_Text textBox;
    public int inputNum = 0;
    public InputActionReference progress;
    public InputActionReference Response;

    void Start()
    {
       
        currentPassageIndex = 0;
    }


    void Update()
    {
        getResponseNumber();
        // Handle w/response progress
        /*
        for (int responseIndex = 0; responseIndex < responseButtons.Length; responseIndex++)
        {
            if (Input.GetKeyDown(responseButtons[responseIndex]))
            {
                currentPassageIndex = currentDialogue.getResponseNextPassage(currentPassageIndex, responseIndex);
                loadText();
                break;
            }
        }
        */
    }

    public int getResponseNumber()
    {
        if (Response.action.triggered)
        {
            int responseNum = (int)Mathf.Round(Response.action.ReadValue<float>());
            if (responseNum != 0)
            {
                Debug.Log("A: " + responseNum);
            }

            return responseNum;

        }

        return -1;
    }
    private void OnEnable()
    {
        progress.action.started += progressNoResponse;
    }
    private void OnDisable()
    {
        progress.action.started -= progressNoResponse; 
    }
    private void progressNoResponse(InputAction.CallbackContext context)
    {
        Debug.Log("Progress!");
        if (currentDialogue.getCurrentResponses(currentPassageIndex) > 0)
        {
            Debug.LogWarning("Will only progress via dialouge button if no responses available");
        }
        else
        {
            currentPassageIndex = currentDialogue.getNoResponseNextPassage(currentPassageIndex);
            loadText();
        }
    }
    private void loadText()
    {
        currentText = currentDialogue.getCurrentText(currentPassageIndex);

    }
}
