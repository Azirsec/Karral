using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuAccess : MonoBehaviour
{
    [SerializeField] Image playImage;
    [SerializeField] Image pauseImage;

    [SerializeField] Image unmutedImage;
    [SerializeField] Image mutedImage;

    [SerializeField] GameObject resetButton;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            restartLevel();
        }
    }

    public void loadPreviousHub()
    {
        SceneManager.LoadScene(HubStorage.currentHub);
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void togglePause()
    {
        if (Time.timeScale == 0)
        {
            //unpause
            Time.timeScale = 1;
            playImage.gameObject.SetActive(false);
            pauseImage.gameObject.SetActive(true);
            resetButton.gameObject.SetActive(false);
        }
        else
        {
            //pause
            Time.timeScale = 0;
            playImage.gameObject.SetActive(true);
            pauseImage.gameObject.SetActive(false);
            resetButton.gameObject.SetActive(true);
        }
    }

    public void resetSave()
    {
        SaveFunctions.resetGame();
        SaveFunctions.LoadGame();
        togglePause();
        SceneManager.LoadScene(HubStorage.currentHub);
    }
}
