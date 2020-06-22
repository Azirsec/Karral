using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubStorage : MonoBehaviour
{
    [SerializeField] GameObject player;

    public static Vector3 playerPositionHumanHub;
    public static Vector3 playerPositionGorillaHub;

    [SerializeField] GameObject[] humanLevelUnlocks = new GameObject[5];
    public static bool[] humanlevelCompleted = new bool[5];

    [SerializeField] GameObject[] gorillaLevelUnlocks = new GameObject[4];
    public static bool[] gorillaLevelCompleted = new bool[4];


    // Start is called before the first frame update
    void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (playerPositionHumanHub != Vector3.zero)
            {
                player.transform.position = playerPositionHumanHub;
            }

            for (int i = 0; i < 5; i++)
            {
                print(humanlevelCompleted[i]);
                if (humanLevelUnlocks[i] != null)
                {
                    humanLevelUnlocks[i].SetActive(humanlevelCompleted[i]);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (playerPositionGorillaHub != Vector3.zero)
            {
                player.transform.position = playerPositionGorillaHub;
            }

            for (int i = 0; i < 5; i++)
            {
                if (gorillaLevelUnlocks[i] != null)
                {
                    gorillaLevelUnlocks[i].SetActive(gorillaLevelCompleted[i]);
                }
            }
        }
    }

    public void savePlayerPositionHumanHub(Transform door)
    {
        HubStorage.playerPositionHumanHub = new Vector3(door.position.x, door.position.y, 0);
    }

    public void savePlayerPositionGorillaHub(Transform door)
    {
        HubStorage.playerPositionGorillaHub = new Vector3(door.position.x, door.position.y, 0);
    }

    public void completeHumanLevel(int level)
    {
        switch (level)
        {
            case 1:
                HubStorage.humanlevelCompleted[0] = true;
                break;
            case 2:
                HubStorage.humanlevelCompleted[1] = true;
                break;
            case 3:
                HubStorage.humanlevelCompleted[2] = true;
                break;
            case 4:
                HubStorage.humanlevelCompleted[3] = true;
                break;
            case 5:
                HubStorage.humanlevelCompleted[4] = true;
                break;
            default:
                break;
        }
    }

    public void completeGorillaLevel(int level)
    {
        switch (level)
        {
            case 1:
                HubStorage.gorillaLevelCompleted[0] = true;
                break;
            case 2:
                HubStorage.gorillaLevelCompleted[1] = true;
                break;
            case 3:
                HubStorage.gorillaLevelCompleted[2] = true;
                break;
            case 4:
                HubStorage.gorillaLevelCompleted[3] = true;
                break;
            default:
                break;
        }
    }
}
