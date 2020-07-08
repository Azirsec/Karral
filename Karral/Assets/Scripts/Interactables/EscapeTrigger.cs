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
            doorway.GetComponent<MeshRenderer>().enabled = false;
            doorway.GetComponent<BoxCollider>().enabled = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHuman>() != null)
        {
            doorway.SetActive(true);
            doorway.GetComponent<MeshRenderer>().enabled = true;
            doorway.GetComponent<BoxCollider>().enabled = true;
        }
    }
}
