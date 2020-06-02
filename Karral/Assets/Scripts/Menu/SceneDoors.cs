using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDoors : MonoBehaviour
{
    [SerializeField] string scene;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerHuman>() != null)
        {
            if (Input.GetKey(KeyCode.E))
            {
                SceneManager.LoadScene(scene);
            }
        }
    }
}
