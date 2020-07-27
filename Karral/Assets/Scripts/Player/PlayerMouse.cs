using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    float movedir;

    [SerializeField] GameObject mesh;

    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;

    bool wallwalking = false;

    float jumptimer = 0.1f;

    bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        GetComponent<BasicMovement>().basicMovement(maxSpeed, accelerationDuration, decelerationDuration, grounded);
        if (wallwalking)
        {
            wallMovement();
        }
        jumptimer -= Time.deltaTime;
    }

    void wallMovement()
    {
        movedir = Input.GetAxisRaw("Vertical");

        //trying to move right
        if (movedir > 0.15f)
        {
            // if already moving left, decelerate
            if (GetComponent<Rigidbody>().velocity.y < 0)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0, maxSpeed / decelerationDuration * Time.deltaTime, 0);
            }
            //if not moving left, accelerate
            else if (GetComponent<Rigidbody>().velocity.y < maxSpeed)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0, maxSpeed / accelerationDuration * Time.deltaTime, 0);
            }
        }
        //trying to move left
        else if (movedir < -0.15f)
        {
            // already moving right, decelerate
            if (GetComponent<Rigidbody>().velocity.y > 0)
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(0, maxSpeed / decelerationDuration * Time.deltaTime, 0);
            }
            // already moving left, accelerate
            else if (GetComponent<Rigidbody>().velocity.y > -maxSpeed)
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(0, maxSpeed / accelerationDuration * Time.deltaTime, 0);
            }
        }
        // not trying to move in any direction
        else
        {
            // if already slow, set velocity to 0
            if (Mathf.Abs(GetComponent<Rigidbody>().velocity.y) < (maxSpeed / decelerationDuration * Time.deltaTime) * 2)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, 0);
            }
            else
            {
                // if not slow, slow down
                GetComponent<Rigidbody>().velocity -= new Vector3(
                        0,
                        GetComponent<Rigidbody>().velocity.y / Mathf.Abs(GetComponent<Rigidbody>().velocity.y) *
                        maxSpeed / decelerationDuration * Time.deltaTime,
                        0);
            }
        }

    }

    private void Jump(Vector3 jumpDirection)
    {
        if (jumptimer <= 0f)
        {
            GetComponent<Rigidbody>().velocity += jumpDirection * jumpSpeed;
            jumptimer = 0.1f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (enabled)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    Jump(collision.contacts[i].normal);
                }
            }

            for (int i = 0; i < collision.contactCount; i++)
            {
                if (Mathf.Abs(collision.contacts[i].normal.x) > 0.9)
                {
                    wallwalking = true;
                    GetComponent<Rigidbody>().useGravity = false;
                }
            }

            grounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (enabled)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }

        wallwalking = false;
        grounded = false;
    }

    public void Activate()
    {
        enabled = true;

        GetComponent<Rigidbody>().mass = 0.75f;
        GetComponent<CapsuleCollider>().height = 1;
        mesh.SetActive(true);
    }

    public void Deactivate()
    {
        wallwalking = false;
        GetComponent<Rigidbody>().useGravity = true;
        enabled = false;
        mesh.SetActive(false);
    }
}
