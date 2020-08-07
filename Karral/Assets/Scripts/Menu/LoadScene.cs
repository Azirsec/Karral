using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadScene : MonoBehaviour
{
    public HubStorage load;

    [SerializeField] GameObject continueButton;

    [SerializeField] GameObject confirmNewGame;

    // Start is called before the first frame update
    void Start()
    {
        continueButton.SetActive(File.Exists(SaveFunctions.path));
    }

    public void newGameClick()
    {
        if (!continueButton.activeInHierarchy)
        {
            startNewGame();
        }
        else
        {
            confirmNewGame.SetActive(true);
        }
    }

    public void startNewGame()
    {
        load.resetGame();
        load.loadGame();
    }

    public void settingsClick()
    {
        // probably unneccesary
    }
}
