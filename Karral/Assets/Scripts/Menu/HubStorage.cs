using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HubStorage : MonoBehaviour
{
    [SerializeField] GameObject player;

    public static Vector3 playerPositionHumanHub;
    public static Vector3 playerPositionGorillaHub;
    public static Vector3 playerPositionMouseHub;
    public static Vector3 playerPositionRhinoHub;
    public static Vector3 playerPositionEagleHub;

    [SerializeField] GameObject[] humanLevelUnlocks = new GameObject[5];
    public static bool[] humanlevelCompleted = new bool[5];

    [SerializeField] GameObject[] gorillaLevelUnlocks = new GameObject[4];
    public static bool[] gorillaLevelCompleted = new bool[4];

    [SerializeField] GameObject[] mouseLevelUnlocks = new GameObject[4];
    public static bool[] mouseLevelCompleted = new bool[4];

    [SerializeField] GameObject[] rhinoLevelUnlocks = new GameObject[4];
    public static bool[] rhinoLevelCompleted = new bool[4];

    [SerializeField] GameObject[] eagleLevelUnlocks = new GameObject[4];
    public static bool[] eagleLevelCompleted = new bool[4];

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

            for (int i = 0; i < 4; i++)
            {
                if (gorillaLevelUnlocks[i] != null)
                {
                    gorillaLevelUnlocks[i].SetActive(gorillaLevelCompleted[i]);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            if (playerPositionMouseHub != Vector3.zero)
            {
                player.transform.position = playerPositionMouseHub;
            }

            for (int i = 0; i < 4; i++)
            {
                if (mouseLevelUnlocks[i] != null)
                {
                    mouseLevelUnlocks[i].SetActive(mouseLevelCompleted[i]);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 3)
        {
            if (playerPositionRhinoHub != Vector3.zero)
            {
                player.transform.position = playerPositionRhinoHub;
            }

            for (int i = 0; i < 4; i++)
            {
                if (rhinoLevelUnlocks[i] != null)
                {
                    rhinoLevelUnlocks[i].SetActive(rhinoLevelCompleted[i]);
                }
            }
        }

        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            if (playerPositionEagleHub != Vector3.zero)
            {
                player.transform.position = playerPositionEagleHub;
            }

            for (int i = 0; i < 4; i++)
            {
                if (eagleLevelUnlocks[i] != null)
                {
                    eagleLevelUnlocks[i].SetActive(eagleLevelCompleted[i]);
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

    public void savePlayerPositionMouseHub(Transform door)
    {
        HubStorage.playerPositionMouseHub = new Vector3(door.position.x, door.position.y, 0);
    }

    public void savePlayerPositionRhinoHub(Transform door)
    {
        HubStorage.playerPositionRhinoHub = new Vector3(door.position.x, door.position.y, 0);
    }

    public void savePlayerPositionEagleHub(Transform door)
    {
        HubStorage.playerPositionEagleHub = new Vector3(door.position.x, door.position.y, 0);
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

    public void completeMouseLevel(int level)
    {
        switch (level)
        {
            case 1:
                HubStorage.mouseLevelCompleted[0] = true;
                break;
            case 2:
                HubStorage.mouseLevelCompleted[1] = true;
                break;
            case 3:
                HubStorage.mouseLevelCompleted[2] = true;
                break;
            case 4:
                HubStorage.mouseLevelCompleted[3] = true;
                break;
            default:
                break;
        }
    }

    public void completeRhinoLevel(int level)
    {
        switch (level)
        {
            case 1:
                HubStorage.rhinoLevelCompleted[0] = true;
                break;
            case 2:
                HubStorage.rhinoLevelCompleted[1] = true;
                break;
            case 3:
                HubStorage.rhinoLevelCompleted[2] = true;
                break;
            case 4:
                HubStorage.rhinoLevelCompleted[3] = true;
                break;
            default:
                break;
        }
    }

    public void completeEagleLevel(int level)
    {
        switch (level)
        {
            case 1:
                HubStorage.eagleLevelCompleted[0] = true;
                break;
            case 2:
                HubStorage.eagleLevelCompleted[1] = true;
                break;
            case 3:
                HubStorage.eagleLevelCompleted[2] = true;
                break;
            case 4:
                HubStorage.eagleLevelCompleted[3] = true;
                break;
            default:
                break;
        }
    }
}
