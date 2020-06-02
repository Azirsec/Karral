using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum birdState
{
    walking,
    flying
}

public class PlayerEagle : MonoBehaviour
{
    float moveDirX;
    float moveDirY;
    [SerializeField] float speed;
    [SerializeField] float flySpeed;

    [SerializeField] Mesh eagleMesh;

    GameObject heldBox;

    float throwTimer = 0.1f;

    float carryCapacity = 0.7f;

    birdState currentState;

    // Update is called once per frame
    void Update()
    {
        basicMovement();
        updateHeldBox();
    }

    void basicMovement()
    {
        switch (currentState)
        {
            case birdState.walking:
                moveDirX = Input.GetAxisRaw("Horizontal");

                GetComponent<Rigidbody>().AddForce(new Vector3(moveDirX * speed, 0, 0), ForceMode.Force);

                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.9f, GetComponent<Rigidbody>().velocity.y, 0);

                break;

            case birdState.flying:
                moveDirX = Input.GetAxisRaw("Horizontal");

                if (Input.GetKey(KeyCode.Space))
                {
                    moveDirY = 1;
                }
                else
                {
                    moveDirY = 0;
                }

                GetComponent<Rigidbody>().AddForce(new Vector3(moveDirX * flySpeed, moveDirY * flySpeed, 0), ForceMode.Force);

                GetComponent<Rigidbody>().velocity *= 0.98f;
                break;
        }

    }

    private void updateHeldBox()
    {
        throwTimer -= Time.deltaTime;

        if (heldBox != null)
        {
            heldBox.GetComponent<Rigidbody>().velocity = GetComponent<Rigidbody>().velocity;

            switch (currentState)
            {
                case birdState.walking:
                    heldBox.transform.position = transform.position + Vector3.up * 1.2f + new Vector3(0, heldBox.transform.localScale.y / 2f, 0);
                    break;

                case birdState.flying:
                    heldBox.transform.position = transform.position - Vector3.up * 1.2f - new Vector3(0, heldBox.transform.localScale.y / 2f, 0);
                    break;
            }

            if (Input.GetKeyDown(KeyCode.E) && throwTimer <= 0)
            {
                dropBox();
            }
        }
    }
    private void Jump()
    {
        GetComponent<Rigidbody>().AddForce(new Vector3(0, speed * 0.5f, 0), ForceMode.Impulse);
    }

    private void dropBox()
    {
        if (heldBox != null)
            heldBox.GetComponent<BoxCollider>().enabled = true;
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
                    currentState = birdState.walking;
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Jump();
                    }
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (enabled)
        {
            currentState = birdState.flying;
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
                        currentState = birdState.flying;
                        heldBox = other.gameObject;
                        heldBox.GetComponent<BoxCollider>().enabled = false;
                    }
                }
            }
        }
    }

    public void Activate()
    {
        enabled = true;
        GetComponent<Rigidbody>().mass = 0.5f;
        GetComponent<MeshFilter>().mesh = eagleMesh;
    }

    public void Deactivate()
    {
        dropBox();
        enabled = false;
    }
}
