using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WheelSelect : MonoBehaviour
{
    [SerializeField] Image image;

    [SerializeField] PlayerHuman human;
    [SerializeField] PlayerGorilla gorilla;
    [SerializeField] PlayerEagle eagle;
    [SerializeField] PlayerRhino rhino;
    [SerializeField] PlayerMouse mouse;

    [SerializeField] GameObject gorillaCover;
    [SerializeField] GameObject mouseCover;
    [SerializeField] GameObject rhinoCover;
    [SerializeField] GameObject eagleCover;

    // Update is called once per frame
    void Update()
    {
        // show wheel select
        if (Input.GetMouseButtonDown(1))
        {
            image.enabled = true;
            GetComponent<RectTransform>().position = Input.mousePosition;
            
            updateCovers();
            
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
        // select animal and hide wheel select
        if (Input.GetMouseButtonUp(1))
        {
            image.enabled = false;
            
            chooseAnimal();
            hideCovers();

            Time.timeScale = 1;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    void chooseAnimal()
    {
        Vector2 mouseDir = new Vector2(Input.mousePosition.x - GetComponent<RectTransform>().position.x, Input.mousePosition.y - GetComponent<RectTransform>().position.y);

        float mouseAng = Vector2.SignedAngle(mouseDir, new Vector2(0, 1));

        if (mouseDir.magnitude < 20)
        {
            // do nothing
        }
        else if (Mathf.Abs(mouseAng) <= 36)
        {
            if (HubStorage.humanDNA)
            {
                DeactivateAll();
                human.Activate();
            }
        }
        else if (mouseAng > 36 && mouseAng <= 108)
        {
            if (HubStorage.gorillaDNA)
            {
                DeactivateAll();
                gorilla.Activate();
            }
        }
        else if (mouseAng > 108)
        {
            if (HubStorage.mouseDNA)
            {
                DeactivateAll();
                mouse.Activate();
            }
        }
        else if (mouseAng < -36 && mouseAng >= -108)
        {
            if (HubStorage.eagleDNA)
            {
                DeactivateAll();
                eagle.Activate();
            }
        }
        else if (mouseAng < -108)
        {
            if (HubStorage.rhinoDNA)
            {
                DeactivateAll();
                rhino.Activate();
            }
        }
    }

    void DeactivateAll()
    {
        human.Deactivate();
        gorilla.Deactivate();
        eagle.Deactivate();
        rhino.Deactivate();
        mouse.Deactivate();
    }

    void updateCovers()
    {
        gorillaCover.SetActive(!HubStorage.gorillaDNA);
        mouseCover.SetActive(!HubStorage.mouseDNA);
        rhinoCover.SetActive(!HubStorage.rhinoDNA);
        eagleCover.SetActive(!HubStorage.eagleDNA);
    }

    void hideCovers()
    {
        gorillaCover.SetActive(false);
        mouseCover.SetActive(false);
        rhinoCover.SetActive(false);
        eagleCover.SetActive(false);
    }
}
