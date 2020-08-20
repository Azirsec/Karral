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
            //GetComponent<MeshRenderer>().enabled = false;
            //GetComponent<BoxCollider>().enabled = false;
            gameObject.SetActive(false);
            open = true;
        }
    }

    public void unlock(List<KeyColour> keys)
    {
        if (redLocked && keys.Count >= 1)
        {
            foreach (KeyColour colour in keys.ToArray())
            {
                if (colour == KeyColour.red)
                {
                    keys.Remove(colour);
                    redLocked = false;
                }
            }
        }
        if (blueLocked && keys.Count >= 1)
        {
            foreach (KeyColour colour in keys.ToArray())
            {
                if (colour == KeyColour.blue)
                {
                    keys.Remove(colour);
                    blueLocked = false;
                }
            }
        }
        if (greenLocked && keys.Count >= 1)
        {
            foreach (KeyColour colour in keys.ToArray())
            {
                if (colour == KeyColour.green)
                {
                    keys.Remove(colour);
                    greenLocked = false;
                }
            }
        }
    }
}
