using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] bool redLocked;
    [SerializeField] bool blueLocked;
    [SerializeField] bool greenLocked;

    bool open;

    public void interact()
    {
        if (!redLocked && !blueLocked && !greenLocked && !open)
        {
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
            open = true;
        }
        else if (open)
        {
            GetComponent<MeshRenderer>().enabled = true;
            GetComponent<BoxCollider>().enabled = true;
            open = false;
        }
    }

    public void unlock(GameObject key, int colour)
    {
        switch (colour)
        {
            case 0:
                if (redLocked)
                {
                    redLocked = false;
                    Destroy(key);
                }
                break;
            case 1:
                if (blueLocked)
                {
                    blueLocked = false;
                    Destroy(key);
                }
                break;
            case 2:
                if (greenLocked)
                {
                    greenLocked = false;
                    Destroy(key);
                }
                break;
        }
    
    }
}
