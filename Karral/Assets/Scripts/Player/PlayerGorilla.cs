using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGorilla : MonoBehaviour
{
    float movedir;
    int faceDirection = 1;
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] float strength;

    [SerializeField] Mesh gorillaMesh;

    GameObject heldBox;

    float throwTimer = 0.1f;

    float jumptimer = 0.1f;

    float fallMultiplier = 10;

    float carryCapacity = 6.5f;

    // Update is called once per frame
    void Update()
    {
        basicMovement();
        updateHeldBox();
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

    private void updateHeldBox()
    {
        throwTimer -= Time.deltaTime;

        if (heldBox != null)
        {
            heldBox.GetComponent<BoxCollider>().enabled = false;

            heldBox.transform.position = transform.position + Vector3.up * 2 + new Vector3(0, heldBox.transform.localScale.y / 2f, 0);
            heldBox.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;

            if (Input.GetKeyDown(KeyCode.E) && throwTimer <= 0)
            {
                throwBox();
            }
        }
    }

    private void throwBox()
    {
        heldBox.GetComponent<Rigidbody>().AddForce(new Vector3(faceDirection * strength, strength / 4, 0), ForceMode.Impulse);
        throwTimer = 0.1f;
        heldBox.GetComponent<BoxCollider>().enabled = true;

        heldBox = null;
    }

    private void dropBox()
    {
        if (heldBox != null)
        {
            heldBox.GetComponent<BoxCollider>().enabled = true;
        }

        heldBox = null;
    }

    private void OnCollisionStay(Collision collision)
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

    private void OnTriggerStay(Collider other)
    {
        if (enabled)
        {
            if (other.GetComponent<Box>() != null && heldBox == null)
            {
                if (Input.GetKeyDown(KeyCode.E) && throwTimer < 0)
                {
                    if (other.GetComponent<Rigidbody>().mass <= carryCapacity)
                    {
                        if (heldBox != null)
                        {
                            dropBox();
                        }
                        throwTimer = 0.05f;
                        heldBox = other.gameObject;
                    }
                }
            }
            if (other.GetComponent<Door>() != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    other.GetComponent<Door>().interact();
                }
            }
        }
    }

    public void Activate()
    {
        enabled = true;

        GetComponent<Rigidbody>().mass = 4;
        GetComponent<MeshFilter>().mesh = gorillaMesh;
        GetComponent<CapsuleCollider>().height = 2;
    }

    public void Deactivate()
    {
        dropBox();
        enabled = false;
        throwTimer = 0.2f;
        jumptimer = 0.1f;
    }
}
