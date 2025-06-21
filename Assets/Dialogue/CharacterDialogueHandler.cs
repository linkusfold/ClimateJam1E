using System;
using System.Linq;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

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
    public GameObject characterArtPrefab;
    private GameObject[] responseButtonInstances;
    private Image characterArtHolder;
    int maxResponseCount = 5;
    [SerializeField]
    Canvas canvas;
    GameObject buttonOBJ;
    void Start()
    {
        CreateArtPrefab();

        responseButtonInstances = new GameObject[maxResponseCount];
        LoadPassage(0);
    }

    public void LoadPassage(int targetPassageIndex)
    {
        currentPassageIndex = targetPassageIndex;
        currentText = currentDialogue.getCurrentText(currentPassageIndex);
        textBox.text = currentText;

        LoadArt();
        


        for (int childIndex = 0; childIndex < textBox.transform.childCount; childIndex++)
        {
            // Debug.Log("Checking children: " + textBox.transform.childCount);
            Transform child = textBox.transform.GetChild(childIndex);
            if (child != null)
            {
                if (child.CompareTag("Response"))
                {
                    Destroy(child.gameObject);

                }
            }
        }

        CreateResponses();
    }
    private void CreateArtPrefab()
    {
        GameObject art = Instantiate(characterArtPrefab, textBox.transform);
        RectTransform artRect = art.GetComponent<RectTransform>();
        Vector2 placePos = Vector2.zero;
        placePos.x -= (artRect.rect.width - textBox.rectTransform.rect.width) / 2;
        placePos.y += (artRect.rect.height + textBox.rectTransform.rect.height) / 2;
        artRect.anchoredPosition = placePos;
        characterArtHolder = art.GetComponent<Image>();
    }
    private void LoadArt()
    {
        characterArtHolder.sprite = currentDialogue.getPassageArt(currentPassageIndex);
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
            Debug.Log("Loading!");
            LoadPassage(currentDialogue.GetNextPassageIndex(currentPassageIndex, responseNumber - 1));
        }
        else if (responseNumber >= 0)
        {
            Debug.LogWarning("Response failed");
        }



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
            LoadPassage(currentDialogue.GetNextPassageIndex(currentPassageIndex));
        }
    }

    void CreateResponses()
    {
        int responseCount = currentDialogue.GetResponseCount(currentPassageIndex);
        RectTransform parentRect = textBox.GetComponent<RectTransform>();

        for (int responseIndex = 0; responseIndex < responseCount; responseIndex++)
        {
            Debug.Log("Run : " + responseIndex);
            parentRect = CreateResponse(parentRect, responseIndex).GetComponent<RectTransform>();

        }

    }

    GameObject CreateResponse(RectTransform previousRectTransform, int responseIndex)
    {
        GameObject response = Instantiate(responseButtonPrefab, previousRectTransform.transform);
        RectTransform responseRect = response.GetComponent<RectTransform>();

        Vector2 placePos = Vector2.zero;
        placePos.x += (responseRect.rect.width - previousRectTransform.rect.width) / 2;
        placePos.y -= (responseRect.rect.height + previousRectTransform.rect.height) / 2;
        responseRect.anchoredPosition = placePos;

        response.GetComponentInChildren<TMP_Text>().text = (responseIndex + 1) + ". " + currentDialogue.GetResponseText(currentPassageIndex, responseIndex);

        response.GetComponent<Button>().onClick.AddListener(() => LoadPassage(currentDialogue.GetResponseTarget(currentPassageIndex, responseIndex)));

        responseButtonInstances[responseIndex] = response;
        return response;
    }

    void buttonTest(int a)
    {
        Debug.Log("Triggered! : " + a);
    }
}
