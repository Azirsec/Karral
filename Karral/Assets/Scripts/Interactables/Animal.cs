using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Animal : MonoBehaviour
{
    [SerializeField] int DNAcode;
   
    public void interact()
    {
        switch(DNAcode)
        {
            case 0:
                //human unlock
                GlobalStorage.humanDNA = true;
                break;

            case 1:
                //Gorilla unlock
                GlobalStorage.gorillaDNA = true;
                break;

            case 2:
                //eagle unlock
                GlobalStorage.eagleDNA = true;
                break;

            case 3:
                //rhino unlock
                GlobalStorage.rhinoDNA = true;
                break;

            case 4:
                //mouse unlock
                GlobalStorage.mouseDNA = true;
                break;
        }

        //exit level, return to main menu
        SceneManager.LoadScene(0);
    }
}
