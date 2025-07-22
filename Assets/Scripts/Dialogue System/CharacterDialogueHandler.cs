using System;
using System.Collections;
using Game_Manager;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class CharacterDialogueHandler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Dialogue Obj, indexes, and textbox
    IStoryListener storyListener;
    [SerializeField]
    public CharacterDialogue currentDialogue;
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
    [SerializeField]
    public Homes characterHouseType;
    public CharacterHouse house;
    public int fitWidth = 200;
    // NEW: drag your disabled “HouseInteriorPanel” here
    [Header("Interior Panel (UI)")]
    [SerializeField]
    private GameObject interiorPanel; 

    void Awake()
    {
        textAudioSource = GetComponent<AudioSource>();
        responseInput = responseActionReference;
        textAudioSource.clip = textSound;
        responseButtonInstances = new GameObject[maxResponseCount];
        SetupArtHolder();
        house = GetComponent<CharacterHouse>();
        characterHouseType = house.characterHouseType;
        storyListener = GetComponent<IStoryListener>();

        // NEW: ensure it starts hidden
        if (interiorPanel != null)            
            interiorPanel.SetActive(false);
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


    #region Enable/Disable

    public void StartDialogue()
    {
        textBox.gameObject.SetActive(true);

        // NEW: show the interior art/UI
        if (interiorPanel != null)            
            interiorPanel.SetActive(true);

        Debug.Log("Starting Dialogue! " + gameObject.name);
        LoadPassage(0);

    }

    public void StopDialogue()
    {
        Debug.Log("Stopping Dialogue! " + gameObject.name);
        textBox.gameObject.SetActive(false);

    }
    
    #endregion


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
    
    #region Response Handling
    
    private void HandleResponse(int passageIndex, int responseIndex)
    {
        // Debug.Log("Handling response!");
        if (storyListener != null)
        {
            storyListener.CheckResponse(currentDialogue.GetResponse(passageIndex, responseIndex));
        }
        
        if (!textBox.text.Equals(currentDialogue.getCurrentText(currentPassageIndex)))
        {
            // Debug.Log("Skipping text load!");
            SkipTextLoading();
            return;
        }


        if (currentDialogue.responseEndsConversation(passageIndex, responseIndex))
        {
            EndConversation();
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
        
        
        // Debug.Log("Response not null!");
        if (response.nextConversation != null)
        {
            Debug.Log("Loading new convo!");
            LoadNewConversation(response.nextConversation);
            return;
        }
        // Debug.Log("Not losading new convo!");

        LoadPassage(response.nextPassageIndex);

        

        //Debug.LogWarning("Tried to load null response!");
    }

    #endregion


    #region Dialogue Loading
    private void EndConversation()
    {
        // Only called when dialogue of a conversation is fully completed

        // TODO: unlock tower if locked.
        // TODO: upgrade tower one level
        // TODO: heal house damage


        // TODO: update info board
       // GameManager.instance.houses.ElementAt((int) Homes.archie);
       
       // NEW: hide it again when dialogue ends
        if (interiorPanel != null)            
            interiorPanel.SetActive(false);

        closeConversation();
    }



    private void closeConversation()
    {
        //Should pause conversation and close out the ui. 
        house.EndConversation();
    }
    private void LoadNewConversation(CharacterDialogue conversation)
    {
        //Debug.Log("Loading passage!");
        //Debug.Log("first new passage: " + conversation.GetPassage(0).text);
        SkipTextLoading();

        currentDialogue = conversation;
        
        LoadPassage(0);
    }

    public void LoadPassage(int targetPassageIndex)
    {
        currentPassageIndex = targetPassageIndex;


        LoadArt();
        DestroyResponses();
        CreateResponses();

        Debug.Log("Starting routine!");
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
    
    public void SkipTextLoading()
    {
         Debug.Log("Skipping text loading!");

        if (currentDialogue == null)
        {
            Debug.Log("Null dialogue");
            return;
        }
        if (!textBox.text.Equals(currentDialogue.getCurrentText(currentPassageIndex)))
            {
                Debug.Log("Skipping!");
                StopCoroutine(loopRoot);
                textBox.text = currentDialogue.getCurrentText(currentPassageIndex);
                return;

            }
    }

    private void LoadArt()
    {

        if (currentDialogue.getPassageArt(currentPassageIndex) == null)
        {
            Debug.Log("Null art!");
            return;
        }
    
        Sprite characterSprite = currentDialogue.getPassageArt(currentPassageIndex);
        characterArtHolder.sprite = characterSprite;
        RectTransform imageRectTransform = characterArtHolder.rectTransform;
        
        float sizeFactor = characterSprite.rect.width / fitWidth;
        characterArtHolder.rectTransform.sizeDelta = new Vector2(characterSprite.rect.width / sizeFactor, characterSprite.rect.height/sizeFactor);
        AnchorTopLeft(textBox.rectTransform, characterArtHolder.rectTransform);
    }

    private void AnchorTopLeft(RectTransform parentRect, RectTransform childRect)
    {
        Vector2 placePos = Vector2.zero;
        placePos.x -= (childRect.rect.width - parentRect.rect.width) / 2;
        placePos.y += (childRect.rect.height + parentRect.rect.height) / 2;
        childRect.anchoredPosition = placePos;
    }

    #endregion


    #region UI Instaniation/Destruction

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
        // Debug.Log("r text " + response.GetComponentInChildren<TMP_Text>().text);
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


        AnchorTopLeft(textBox.rectTransform, artRect);
        characterArtHolder = art.GetComponent<Image>();
    }

    #endregion
}
