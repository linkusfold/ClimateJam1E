using UnityEngine;

public class GameManagerTester : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.instance.doThing();
    }

}
