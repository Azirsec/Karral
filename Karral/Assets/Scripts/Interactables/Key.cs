using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] int colour = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Door>() != null)
        {
            collision.gameObject.GetComponent<Door>().unlock(gameObject, colour);
        }
    }
}
