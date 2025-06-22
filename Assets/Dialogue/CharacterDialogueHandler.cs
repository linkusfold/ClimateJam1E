using System;
using System.Collections;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.UI;

public class CharacterDialogueHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Dialogue Obj, indexes, and textbox
    [SerializeField]
    CharacterDialogue currentDialogue;
    int currentPassageIndex;
    [Header("Text Settings")]
    [SerializeField]
    float textSpeed = 0.1f;
    public TMP_Text textBox;
    [Header("Sound Settings")]
    [SerializeField]
    float ptichVariation;
    [SerializeField]
    AudioClip textSound;
    [SerializeField]
    AudioSource textAudioSource;
    [Header("Input Settings")]
    public InputActionReference progress;
    public InputActionReference Response;
    [Header("Prefabs")]
    [SerializeField]
    GameObject responseButtonPrefab;
    [SerializeField]
    GameObject characterArtPrefab;
    private GameObject[] responseButtonInstances;
    private Image characterArtHolder;
    int maxResponseCount = 5;
    [SerializeField]
    Canvas canvas;
    GameObject buttonOBJ;
    Coroutine loopRoot;
    void Start()
    {
        CreateArtPrefab();
        //textSpeed = textSound.length;
        textAudioSource.clip = textSound;
        responseButtonInstances = new GameObject[maxResponseCount];
        LoadPassage(0);
    }

    public void LoadPassage(int targetPassageIndex)
    {
        currentPassageIndex = targetPassageIndex;
        

        LoadArt();
        DestroyResponses();
        CreateResponses();

        
        loopRoot = StartCoroutine(LoadText());
    }

    IEnumerator LoadText()
    {
        String currentText = currentDialogue.getCurrentText(currentPassageIndex);
        textBox.text = "";
        foreach (char c in currentText.ToCharArray())
        {
            textBox.text += c;
            textAudioSource.pitch = UnityEngine.Random.Range(1 - ptichVariation, 1 + ptichVariation);
            textAudioSource.Play();
            yield return new WaitForSeconds(textSpeed);

        }
        yield break;
    }

    private void DestroyResponses()
    {
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
        int responseIndex = getResponseNumberIndex();
        
        if (responseIndex >= 0 && responseIndex <= currentDialogue.GetResponseCount(currentPassageIndex))
        {
            Debug.Log("Loading!");
            HandleResponse(currentPassageIndex, responseIndex);
        }
        else if (responseIndex >= 0)
        {
            Debug.LogWarning("Response failed");
        }



    }

    public int getResponseNumberIndex()
    {
        if (Response.action.triggered)
        {
            int responseNum = (int)Mathf.Round(Response.action.ReadValue<float>());
            if (responseNum != 0)
            {
                Debug.Log("Response Number Index : " + (responseNum -1));
            }

            return responseNum- 1;

        }

        return -1;
    }

    private bool HandleTextLoadSkip()
    {
        if (textBox.text != currentDialogue.getCurrentText(currentPassageIndex))
        {

            Debug.Log("Skipping text loading!");
            StopCoroutine(loopRoot);
            textBox.text = currentDialogue.getCurrentText(currentPassageIndex);
            return true;
        }

        return false;
    }
    private void ProgressNoResponse(InputAction.CallbackContext context)
    {
        Debug.Log("Progress!");

        if (HandleTextLoadSkip()) return;

        if (currentDialogue.GetResponseCount(currentPassageIndex) > 0)
        {
            Debug.LogWarning("Will only progress via dialouge button if no responses available");
        }
        else
        {
            Debug.Log("Progressing, no responses");
            Passage currentPassage = currentDialogue.GetPassage(currentPassageIndex);
            if (currentPassage.nextConversation != null)
            {
                Debug.Log("Not null!");

                LoadNewConversation(currentPassage.nextConversation);
                return;
            }

            LoadPassage(currentDialogue.GetNextPassageIndex(currentPassageIndex));
        }
    }

    void CreateResponses()
    {
        int responseCount = currentDialogue.GetResponseCount(currentPassageIndex);
        RectTransform parentRect = textBox.GetComponent<RectTransform>();

        for (int responseIndex = 0; responseIndex < responseCount; responseIndex++)
        {
            
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

        response.GetComponent<Button>().onClick.AddListener(() => HandleResponse(currentPassageIndex, responseIndex));

        responseButtonInstances[responseIndex] = response;
        return response;
    }

    private void LoadNewConversation(CharacterDialogue conversation)
    {
        Debug.Log("Loading passage!");
        Debug.Log("first new passage: " + conversation.GetPassage(0).text);
        currentDialogue = conversation;

        LoadPassage(0);
    }
    private void HandleResponse(int passageIndex, int responseIndex)
    {
        Debug.Log("Handling response!");
        Response response = currentDialogue.GetResponse(passageIndex, responseIndex);
        if (HandleTextLoadSkip()) return;

        if (response != null)
        {
            Debug.Log("Response not null!");
            if (response.nextConversation != null)
            {
                Debug.Log("Loading new convo!");
                LoadNewConversation(response.nextConversation);
                return;
            }
            Debug.LogWarning("Not loading new convo!");
            
            LoadPassage(response.nextPassageIndex);
            
        }

        Debug.LogWarning("Tried to load null response!");
    }


    void buttonTest(int a)
    {
        Debug.Log("Triggered! : " + a);
    }
}
