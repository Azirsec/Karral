using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGorilla : MonoBehaviour
{
    int faceDirection = 1;
    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;

    [SerializeField] float throwVelocity;

    [SerializeField] Mesh gorillaMesh;

    GameObject heldBox;

    float throwTimer = 0.1f;

    float jumptimer = 0.1f;

    bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        movement();
        updateHeldBox();
        Jump();
    }

    void movement()
    {
        float temp = Input.GetAxisRaw("Horizontal");
        if (temp > 0)
        {
            faceDirection = 1;
        }
        else if (temp < 0)
        {
            faceDirection = -1;
        }

        GetComponent<BasicMovement>().basicMovement(maxSpeed, accelerationDuration, decelerationDuration);
    }

    private void Jump()
    {
        jumptimer -= Time.deltaTime;
        if (grounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                if (jumptimer <= 0f)
                {
                    grounded = false;
                    GetComponent<Rigidbody>().velocity += new Vector3(0, jumpSpeed, 0);
                    jumptimer = 0.1f;
                }
            }
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
        heldBox.GetComponent<Rigidbody>().velocity += new Vector3(faceDirection * throwVelocity, 0, 0);
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
        if (enabled)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.contacts[i].normal.y > 0.8)
                {
                    grounded = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
            grounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (enabled)
        {
            if (other.GetComponent<Box>() != null && heldBox == null)
            {
                if (Input.GetKeyDown(KeyCode.E) && throwTimer < 0)
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
