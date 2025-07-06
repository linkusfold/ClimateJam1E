using System;
using System.Collections;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
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
    private AudioSource textAudioSource;
    [Header("Input Settings")]
    public InputActionReference responseActionReference;
    private InputAction responseInput;
    [Header("Prefabs")]
    [SerializeField]
    GameObject responseButtonPrefab;
    private GameObject[] responseButtonInstances;
    private Image characterArtHolder;
    int maxResponseCount = 5;
    GameObject buttonOBJ;
    Coroutine loopRoot;
    void Awake()
    {
        textAudioSource = GetComponent<AudioSource>();
        responseInput = responseActionReference;
        textAudioSource.clip = textSound;
        responseButtonInstances = new GameObject[maxResponseCount];
        SetupArtHolder();

    }
    void Start()
    {   
        //Debug.Log("Made it!");
    }

    public void StartDialogue()
    {
        textBox.gameObject.SetActive(true);
        Debug.Log("Starting Dialogue!");
        LoadPassage(0);

    }

    public void StopDialogue()
    {
        textBox.gameObject.SetActive(false);

    }

    private void InstaniateAudioSource()
    {
        textAudioSource = new AudioSource();
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
    
    void Update()
    {

        // Handle Responses
        int responseIndex = getResponseNumberIndex();

        if (responseIndex >= 0)
        {
            Debug.Log("Loading!");
            HandleResponse(currentPassageIndex, responseIndex);
        }

        

    }

    public int getResponseNumberIndex()
    {
        if (responseInput.WasPressedThisFrame())
        {

            int responseNum = (int)Mathf.Round(responseInput.ReadValue<float>());
            if (responseNum != 0)
            {
                Debug.Log("Response Number Index : " + (responseNum - 1));
            }

            return responseNum - 1;

        }

        return -1;
    }

    private void SkipTextLoading()
    {
        Debug.Log("Skipping text loading!");
        StopCoroutine(loopRoot);
        textBox.text = currentDialogue.getCurrentText(currentPassageIndex);
        return;
    }
    
    #region Response Handling
    
    private void HandleResponse(int passageIndex, int responseIndex)
    {
        Debug.Log("Handling response!");

        if (!textBox.text.Equals(currentDialogue.getCurrentText(currentPassageIndex)))
        {
            Debug.Log("Skipping text load!");
            SkipTextLoading();
            return;
        }

        if (responseIndex <= currentDialogue.GetResponseCount(passageIndex))
        {
            // Debug.LogWarning("No response at index, exiting method.");
            
        }

        Response response = currentDialogue.GetResponse(passageIndex, responseIndex);

        if (response == null)
        {
            Debug.LogWarning("Returning cuz null");
            return;
        }
        
        
        Debug.Log("Response not null!");
        if (response.nextConversation != null)
        {
            Debug.Log("Loading new convo!");
            LoadNewConversation(response.nextConversation);
            return;
        }
        Debug.Log("Not loading new convo!");

        LoadPassage(response.nextPassageIndex);

        

        //Debug.LogWarning("Tried to load null response!");
    }

    #endregion


    #region ScriptableObject Handling

    private void LoadNewConversation(CharacterDialogue conversation)
    {
        Debug.Log("Loading passage!");
        Debug.Log("first new passage: " + conversation.GetPassage(0).text);
        currentDialogue = conversation;

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

    #endregion


    #region UI Elmements

    void CreateResponses()
    {
        int responseCount = currentDialogue.GetResponseCount(currentPassageIndex);
        RectTransform parentRect = textBox.GetComponent<RectTransform>();

        for (int responseIndex = 0; responseIndex < responseCount; responseIndex++)
        {

            parentRect = CreateResponse(parentRect, responseIndex).GetComponent<RectTransform>();

        }

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
    GameObject CreateResponse(RectTransform previousRectTransform, int responseIndex)
    {
        GameObject response = Instantiate(responseButtonPrefab, previousRectTransform.transform);
        //Debug.Log(response.GetComponentInChildren<TMP_Text>().text + "response stuffs");
        RectTransform responseRect = response.GetComponent<RectTransform>();

        Vector2 placePos = Vector2.zero;
        placePos.x += (responseRect.rect.width - previousRectTransform.rect.width) / 2;
        placePos.y -= (responseRect.rect.height + previousRectTransform.rect.height) / 2;
        responseRect.anchoredPosition = placePos;

        response.GetComponentInChildren<TMP_Text>().text = (responseIndex + 1) + ". " + currentDialogue.GetResponseText(currentPassageIndex, responseIndex);

        response.GetComponent<Button>().onClick.AddListener(() => HandleResponse(currentPassageIndex, responseIndex));
        Debug.Log("r text " + response.GetComponentInChildren<TMP_Text>().text);
        if (responseButtonInstances == null)
        {
            Debug.LogError("Response array is null!");
        }
        responseButtonInstances[responseIndex] = response;
        return response;
    }
    
    private void SetupArtHolder()
    {
        characterArtHolder = textBox.GetComponentInChildren<Image>();

        GameObject art = characterArtHolder.gameObject;
        RectTransform artRect = art.GetComponent<RectTransform>();
        Vector2 placePos = Vector2.zero;
        placePos.x -= (artRect.rect.width - textBox.rectTransform.rect.width) / 2;
        placePos.y += (artRect.rect.height + textBox.rectTransform.rect.height) / 2;
        artRect.anchoredPosition = placePos;
        characterArtHolder = art.GetComponent<Image>();
    }
    
    private void LoadArt()
    {
        currentDialogue.getPassageArt(currentPassageIndex);
        Debug.Log(characterArtHolder.name);
        characterArtHolder.sprite = currentDialogue.getPassageArt(currentPassageIndex);
    }

    #endregion
}
