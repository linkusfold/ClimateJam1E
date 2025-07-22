using System;
using System.Linq;
using Game_Manager;
using UnityEngine;

public class ArchiePathListener : MonoBehaviour, IStoryListener
{
    public String[] wantedResponseTexts;
    public void CheckResponse(Response response)
    {
        if (wantedResponseTexts.Contains(response.text))
        {
            GameManager.instance.storyEvents[(int) StoryChoices.archiePath] = 1;
        }
    }
}
