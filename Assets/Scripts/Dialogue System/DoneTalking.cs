using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoneTalking : MonoBehaviour
{
    public CharacterHouse[] characterHouses;
    public CharacterDialogue[] endStateDialogues;
    public static DoneTalking instance;
    public void Awake()
    {
        instance = this;

            
    }
    public void checkSceneLoad()
    {
        foreach (CharacterHouse house in characterHouses)
        {
            if (!endStateDialogues.Contains(house.dialogue))
            {
                // At least one house not finished
                return;
            }
        }

        loadNextScene();


    }

    public static void HandleScene()
    {
        instance.checkSceneLoad();
    }

    private void loadNextScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
