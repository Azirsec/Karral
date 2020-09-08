using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyableWall : MonoBehaviour
{
    [SerializeField] float destructionForce;

    [SerializeField] bool floor;

    [SerializeField] ParticleSystem explodeRight;
    [SerializeField] ParticleSystem explodeLeft;
    [SerializeField] ParticleSystem explodeDown;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.magnitude > destructionForce)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce(-collision.impulse * 0.75f, ForceMode.Impulse);

            if (!floor)
            {
                if (collision.impulse.x < 0)
                {
                    explodeRight.Play();
                }
                else
                {
                    explodeLeft.Play();
                }
            }
            else
            {
                explodeDown.Play();
            }

            GetComponentInChildren<MeshRenderer>().enabled = false;
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}   
