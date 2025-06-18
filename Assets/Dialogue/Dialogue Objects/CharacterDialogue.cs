using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDialogue", menuName = "Scriptable Objects/CharacterDialogue")]
public class CharacterDialogue : ScriptableObject
{
    [SerializeField]
    Passage[] passages;
    public String getCurrentText(int passageIndex)
    {
        return passages[passageIndex].text;
    }

    public int getCurrentResponses(int passageIndex)
    {
        return passages[passageIndex].responses.Length;
    }

    public int getNoResponseNextPassage(int passageIndex)
    {
        return passages[passageIndex].nextPassageIndex;
    }

    public int getResponseNextPassage(int passageIndex, int responseIndex)
    {
        return passages[passageIndex].responses[responseIndex].nextPassageIndex;
    }
}
[Serializable]

public class Passage
{
    [SerializeField]
    public String text;

    [Tooltip("Only used if no responses are set.")]
    [SerializeField]
    public int nextPassageIndex;
    [SerializeField]
    public Response[] responses;
    
}

[Serializable]
public class Response
{
    public String text;
    public int nextPassageIndex;
}
