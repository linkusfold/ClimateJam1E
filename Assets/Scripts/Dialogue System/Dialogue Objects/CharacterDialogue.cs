using System;
using UnityEngine;
using UnityEngine.TextCore.Text;

[CreateAssetMenu(fileName = "CharacterDialogue", menuName = "Scriptable Objects/CharacterDialogue")]
public class CharacterDialogue : ScriptableObject
{
    [SerializeField]
    Passage[] passages;



    public String getCurrentText(int passageIndex)
    {
        return passages[passageIndex].text;
    }

    public int GetResponseCount(int passageIndex)
    {
        return passages[passageIndex].responses.Length;
    }

    public Passage GetPassage(int passageIndex)
    {
        return passages[passageIndex];
    }
    public int GetNextPassageIndex(int passageIndex, int responseIndex)
    {
        // Get next passage from response target
        return passages[passageIndex].responses[responseIndex].nextPassageIndex;
    }

    public String GetResponseText(int passageIndex, int responseIndex)
    {
        return passages[passageIndex].responses[responseIndex].text;
    }

    public int GetResponseTarget(int passageIndex, int responseIndex)
    {
        return passages[passageIndex].responses[responseIndex].nextPassageIndex;
    }

    public Sprite getPassageArt(int passageIndex)
    {
        return passages[passageIndex].sprite;
    }

    public Response GetResponse(int passageIndex, int responseIndex)
    {
        return passages[passageIndex].responses[responseIndex];
    }

    public bool responseEndsConversation(int passageIndex, int responseIndex)
    {
        return GetResponse(passageIndex, responseIndex).endsConversation;
    }

}
[Serializable]

public class Passage
{
    [SerializeField]
    public String text;
    [SerializeField]
    public Response[] responses;
    [SerializeField]
    public Sprite sprite;
}

[Serializable]
public class Response
{
    public String text;
    public int nextPassageIndex;
    public CharacterDialogue nextConversation;
    public bool endsConversation;
}
