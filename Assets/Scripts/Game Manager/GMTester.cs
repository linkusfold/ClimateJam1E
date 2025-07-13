using Game_Manager;
using UnityEngine;

public class GMTester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.doThing();
    }

}
