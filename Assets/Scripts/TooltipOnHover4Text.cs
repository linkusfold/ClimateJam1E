using UnityEngine;
using UnityEngine.UI;

/*
 * -----------------------------------------------
 * RevealImageOnClick.cs
 * Author: [Lauren Thoman]
 * Date: [July 14, 2025]
 *
 * Attach this to the same GameObject as your Button.
 * On click, it will toggle the visibility of another image.
 * -----------------------------------------------
 */
public class RevealImageOnClick : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The UI Image (or any GameObject) to reveal/hide when this button is clicked.")]
    public GameObject imageToReveal;

    private Button _button;

    void Awake()
    {
        // Cache the Button component
        _button = GetComponent<Button>();
        if (_button == null)
        {
            Debug.LogError("RevealImageOnClick requires a Button component on the same GameObject.");
            enabled = false;
            return;
        }

        // Ensure the target image starts hidden
        if (imageToReveal != null)
            imageToReveal.SetActive(false);
    }

    void Start()
    {
        // Subscribe to the click event
        _button.onClick.AddListener(OnButtonClicked);
    }

    void OnDestroy()
    {
        // Clean up listener
        if (_button != null)
            _button.onClick.RemoveListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        if (imageToReveal == null) return;

        // Toggle the image's active state
        bool isActive = imageToReveal.activeSelf;
        imageToReveal.SetActive(!isActive);
    }
}