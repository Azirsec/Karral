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
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-collision.impulse * 0.75f, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }
}   
