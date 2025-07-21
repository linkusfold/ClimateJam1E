using UnityEngine;
using UnityEngine.UI;

public class ButtonClickSound : MonoBehaviour
{
    public AudioSource audioSoruce;
    public AudioClip clickSound;

    public void PlayClickSound()
    {
        if (audioSoruce != null && clickSound != null)
        {
            audioSoruce.PlayOneShot(clickSound);
        }
    }
}
