using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SceneDoors : MonoBehaviour
{
    [SerializeField] string sceneToLoad;
    //[SerializeField] string currentScene;
    [SerializeField] UnityEvent trigger;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerHuman>() != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                trigger.Invoke();
                SceneManager.LoadScene(sceneToLoad);
            }
        }
    }
}
