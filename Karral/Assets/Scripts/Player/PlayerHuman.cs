using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHuman : MonoBehaviour
{
    float movedir;
    int faceDirection = 1;
    [SerializeField] float speed;
    [SerializeField] float jump;

    [SerializeField] Mesh humanMesh;

    List<KeyColour> heldKeys = new List<KeyColour>();

    float jumptimer = 0.1f;

    float fallMultiplier = 10;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        basicMovement();
    }

    void basicMovement()
    {
        movedir = Input.GetAxisRaw("Horizontal");
        if (movedir > 0)
        {
            faceDirection = 1;
        }
        else if (movedir < 0)
        {
            faceDirection = -1;
        }

        GetComponent<Rigidbody>().AddForce(new Vector3(movedir * speed, 0, 0), ForceMode.Force);

        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.9f, GetComponent<Rigidbody>().velocity.y - fallMultiplier * Time.deltaTime, 0);

        jumptimer -= Time.deltaTime;
    }

    private void Jump()
    {
        if (jumptimer <= 0f)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, jump, 0), ForceMode.Impulse);
            jumptimer = 0.1f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (enabled)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.contacts[i].normal.y > 0.8)
                {
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Jump();
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (enabled)
        {
            //open and close door
            if (other.GetComponent<Door>() != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    other.GetComponent<Door>().interact();
                }
            }

            //pick up key
            if (other.GetComponent<Key>() != null)
            {
                heldKeys.Add(other.GetComponent<Key>().getColour());
                other.GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
                other.GetComponent<SphereCollider>().enabled = false;
                //Destroy(other.gameObject);
            }

            //unlock door
            if (other.GetComponent<Door>() != null)
            {
                other.GetComponent<Door>().unlock(heldKeys);
            }
        }
    }

    public void Activate()
    {
        enabled = true;

        GetComponent<Rigidbody>().mass = 1;
        GetComponent<MeshFilter>().mesh = humanMesh;
        GetComponent<CapsuleCollider>().height = 2;
    }

    public void Deactivate()
    {
        enabled = false;
        jumptimer = 0.1f;
    }
}
