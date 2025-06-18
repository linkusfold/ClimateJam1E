using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterDialogue", menuName = "Scriptable Objects/CharacterDialogue")]
public class CharacterDialogue : ScriptableObject
{
    [SerializeField]
    Passage[] passages;
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
