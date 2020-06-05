using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubStorage : MonoBehaviour
{
    [SerializeField] GameObject player;

    public static Vector3 playerPosition;

    [SerializeField] GameObject human1object;
    public static bool human1Complete = false;

    [SerializeField] GameObject gorilla1object;
    public static bool gorilla1Complete = false;

    [SerializeField] MovingPlatform gorilla2platform;
    public static bool gorilla2Complete = false;

    // Start is called before the first frame update
    void Awake()
    {
        if (playerPosition != Vector3.zero)
        {
            player.transform.position = playerPosition;
        }

        human1object.SetActive(human1Complete);

        gorilla1object.SetActive(gorilla1Complete);

        gorilla2platform.enabled = gorilla2Complete;
    }

    public void savePlayerPosition(Transform door)
    {
        HubStorage.playerPosition = new Vector3(door.position.x, door.position.y, 0);
    }

    public static void completeLevel(string level)
    {
        switch (level)
        {
            case "Human 1":
                HubStorage.human1Complete = true;
                break;

            case "Gorilla 1":
                HubStorage.gorilla1Complete = true;
                break;
            case "Gorilla 2":
                HubStorage.gorilla2Complete = true;
                break;



            default:
                break;
        }
    }
}
