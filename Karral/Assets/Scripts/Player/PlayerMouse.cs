using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    float movedir;

    [SerializeField] float speed;
    [SerializeField] float jumpForce;

    bool wallwalking = false;

    float jumptimer = 0.1f;

    float fallMultiplier = 10;

    // Update is called once per frame
    void Update()
    {
        if (!wallwalking)
        {
            basicMovement();
        }
        else
        {
            wallMovement();
        }
    }

    void basicMovement()
    {
        movedir = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody>().AddForce(new Vector3(movedir * speed, 0, 0), ForceMode.Force);

        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.95f, GetComponent<Rigidbody>().velocity.y - fallMultiplier * Time.deltaTime, 0);

        jumptimer -= Time.deltaTime;
    }

    void wallMovement()
    {
        movedir = Input.GetAxisRaw("Vertical");

        GetComponent<Rigidbody>().AddForce(new Vector3(0, movedir * speed * 4, 0), ForceMode.Force);

        GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y * 0.9f, 0);

        wallwalking = false;
        GetComponent<Rigidbody>().useGravity = true;

        jumptimer -= Time.deltaTime;
    }

    private void Jump(Vector3 jumpDirection)
    {
        if (jumptimer <= 0f)
        {
            jumpDirection.x = jumpDirection.x * 2;
            GetComponent<Rigidbody>().AddForce(jumpDirection * jumpForce, ForceMode.Impulse);
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
                    print("hello");
                }
            }
        }
    }

    public void Activate()
    {
        enabled = true;
    }

    public void Deactivate()
    {
        wallwalking = false;
        GetComponent<Rigidbody>().useGravity = true;
        enabled = false;
    }
}
