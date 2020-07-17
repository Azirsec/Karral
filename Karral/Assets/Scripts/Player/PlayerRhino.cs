using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRhino : MonoBehaviour
{
    float movedir;
    int faceDirection;
    [SerializeField] float speed;
    [SerializeField] float jumpForce;
    [SerializeField] float dashForce;

    float jumptimer = 0.1f;
    float dashTimer = 0f;

    float fallMultiplier = 20;

    [SerializeField] Mesh rhinoMesh;

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

        if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > 10 && dashTimer < 1.5f || Mathf.Abs(movedir) < 0.2f)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.95f, GetComponent<Rigidbody>().velocity.y - fallMultiplier * Time.deltaTime, 0);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, GetComponent<Rigidbody>().velocity.y - fallMultiplier * Time.deltaTime, 0);

        }

        if (dashTimer > 2.5f)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.right * faceDirection * dashForce, ForceMode.Force);
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Dash();
        }

        jumptimer -= Time.deltaTime;
        dashTimer -= Time.deltaTime;
    }

    private void Jump()
    {
        if (jumptimer <= 0f)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumptimer = 0.1f;
        }
    }

    private void Dash()
    {
        if (dashTimer <= 0f)
        {
            dashTimer = 3f;
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

    public void Activate()
    {
        enabled = true;

        GetComponent<Rigidbody>().mass = 8;
        GetComponent<MeshFilter>().mesh = rhinoMesh;
        GetComponent<CapsuleCollider>().height = 2;
    }

    public void Deactivate()
    {
        enabled = false;
    }
}
