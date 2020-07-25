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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            image.enabled = true;
            GetComponent<RectTransform>().position = Input.mousePosition;
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }

        if (Input.GetMouseButtonUp(1))
        {
            image.enabled = false;
            chooseAnimal();
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
            if (GlobalStorage.humanDNA)
            {
                DeactivateAll();
                human.Activate();
            }
        }
        else if (mouseAng > 36 && mouseAng <= 108)
        {
            if (GlobalStorage.gorillaDNA)
            {
                DeactivateAll();
                gorilla.Activate();
            }
        }
        else if (mouseAng > 108)
        {
            if (GlobalStorage.eagleDNA)
            {
                DeactivateAll();
                eagle.Activate();
            }
        }
        else if (mouseAng < -36 && mouseAng >= -108)
        {
            if (GlobalStorage.rhinoDNA)
            {
                DeactivateAll();
                rhino.Activate();
            }
        }
        else if (mouseAng < -108)
        {
            if (GlobalStorage.mouseDNA)
            {
                DeactivateAll();
                mouse.Activate();
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
}
