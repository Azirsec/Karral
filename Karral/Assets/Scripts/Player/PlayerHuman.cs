using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHuman : MonoBehaviour
{
    float movedir;
    int faceDirection = 1;
    [SerializeField] float speed;
    [SerializeField] float jump;
    [SerializeField] float strength;

    [SerializeField] Mesh humanMesh;

    GameObject heldBox;

    float throwTimer = 0.1f;

    float jumptimer = 0.1f;

    float fallMultiplier = 10;

    float carryCapacity = 1.5f;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

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
        heldBox.GetComponent<Rigidbody>().AddForce(new Vector3(faceDirection * strength, strength / 2, 0), ForceMode.Impulse);
        throwTimer = 0.1f;
        heldBox = null;
    }

    private void dropBox()
    {
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
            if (other.GetComponent<Animal>() != null)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    other.GetComponent<Animal>().interact();
                }
            }
        }
    }

    public void Activate()
    {
        enabled = true;

        GetComponent<Rigidbody>().mass = 1;
        GetComponent<MeshFilter>().mesh = humanMesh;
    }

    public void Deactivate()
    {
        dropBox();
        enabled = false;
        throwTimer = 0.2f;
        jumptimer = 0.1f;
    }
}
