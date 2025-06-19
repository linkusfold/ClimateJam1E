using System;
using System.Security.Cryptography;
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
    public GameObject responseButtonPrefab;
    [SerializeField]
    Canvas canvas;
    GameObject buttonOBJ;
    void Start()
    {

        currentPassageIndex = 0;
        loadText();
        CreateResponseButtons();
    }


    private void OnEnable()
    {
        progress.action.started += ProgressNoResponse;
    }
    private void OnDisable()
    {
        progress.action.started -= ProgressNoResponse;
    }
    void Update()
    {


        // Handle Responses
        int responseNumber = getResponseNumber();
        if (responseNumber >= 0 && responseNumber <= currentDialogue.GetResponseCount(currentPassageIndex))
        {
            currentPassageIndex = currentDialogue.GetNextPassageIndex(currentPassageIndex, responseNumber - 1);
            loadText();
        }
        else if (responseNumber >= 0)
        {
            Debug.LogWarning("Response failed");
        }

        Debug.Log("Button pos: " + buttonOBJ.GetComponent<RectTransform>().position);
        Debug.Log("Button anchor pos: " + buttonOBJ.GetComponent<RectTransform>().anchoredPosition);

    }

    public int getResponseNumber()
    {
        if (Response.action.triggered)
        {
            int responseNum = (int)Mathf.Round(Response.action.ReadValue<float>());
            if (responseNum != 0)
            {
                Debug.Log("Response Number: " + responseNum);
            }

            return responseNum;

        }

        return -1;
    }
    private void ProgressNoResponse(InputAction.CallbackContext context)
    {
        Debug.Log("Progress!");
        if (currentDialogue.GetResponseCount(currentPassageIndex) > 0)
        {
            Debug.LogWarning("Will only progress via dialouge button if no responses available");
        }
        else
        {
            currentPassageIndex = currentDialogue.GetNextPassageIndex(currentPassageIndex);
            loadText();
        }
    }
    private void loadText()
    {
        currentText = currentDialogue.getCurrentText(currentPassageIndex);
        if (currentDialogue.GetResponseCount(currentPassageIndex) > 0)
        {
        }
        

        textBox.text = currentText;
    }

    void CreateResponseButtons()
    {
        // add responses to text;
        int responseCount = currentDialogue.GetResponseCount(currentPassageIndex);
        RectTransform previousRectTransform = textBox.rectTransform;
        Vector2 debugPos = previousRectTransform.anchoredPosition;
        Debug.Log("prev x: " + debugPos);
        buttonOBJ = Instantiate(responseButtonPrefab, canvas.transform);
        Vector2 placePos = debugPos;
        placePos.x += (buttonOBJ.GetComponent<RectTransform>().rect.width - previousRectTransform.rect.width) / 2;
        placePos.y -= (buttonOBJ.GetComponent<RectTransform>().rect.height + previousRectTransform.rect.height) / 2;
        buttonOBJ.GetComponent<RectTransform>().anchoredPosition = placePos;
        /zzz// left here
    }
}
