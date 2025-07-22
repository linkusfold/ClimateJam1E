using System;
using System.Linq;
using Game_Manager;
using UnityEngine;

public class PocketKnifeListener : MonoBehaviour, IStoryListener
{
    public String[] wantedResponses;
    public void CheckResponse(Response response)
    {
        if (wantedResponses.Contains(response.text))
        {
            GameManager.instance.storyEvents[(int)StoryChoices.givePocketKnife] = 1;
            Debug.LogWarning("Worked!");
        }
    }  


}
