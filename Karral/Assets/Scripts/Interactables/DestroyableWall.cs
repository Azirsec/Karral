using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] float destructionForce;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > destructionForce)
        {
            Destroy(gameObject);
        }
    }
}   
