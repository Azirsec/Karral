using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeTrigger : MonoBehaviour
{
    [SerializeField] GameObject doorway;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHuman>() != null)
        {
            doorway.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHuman>() != null)
        {
            doorway.SetActive(true);
        }
    }
}
