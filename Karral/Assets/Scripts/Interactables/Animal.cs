using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Animal : MonoBehaviour
{
    [SerializeField] int DNAcode;
    [SerializeField] animalUnlockAnimation animalUnlockAnimation;

    public void interact()
    {
        switch (DNAcode)
        {
            case 0:
                //human unlock
                HubStorage.humanDNA = true;
                break;

            case 1:
                //Gorilla unlock
                HubStorage.gorillaDNA = true;
                break;

            case 2:
                //mouse unlock
                HubStorage.mouseDNA = true;
                break;

            case 3:
                //rhino unlock
                HubStorage.rhinoDNA = true;
                break;
            
            case 4:
                //eagle unlock
                HubStorage.eagleDNA = true;
                break;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerHuman>() != null)
        {
            if (other.GetComponent<PlayerHuman>().enabled)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    interact();
                    animalUnlockAnimation.doTheThing();
                }
            }
        }
    }
}
