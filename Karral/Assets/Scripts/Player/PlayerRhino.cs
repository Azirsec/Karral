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
    float dashTimer = 3f;

    float fallMultiplier = 10;

    bool grounded = false;

    [SerializeField] Mesh rhinoMesh;

    // Update is called once per frame
    void Update()
    {
        basicMovement();
    }

    void basicMovement()
    {
        if (grounded)
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

            if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) > 10 || Mathf.Abs(movedir) < 0.2f)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.99f, GetComponent<Rigidbody>().velocity.y - fallMultiplier * Time.deltaTime, 0);
            }
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
            GetComponent<Rigidbody>().AddForce(Vector3.right * faceDirection * dashForce, ForceMode.Impulse);
            dashTimer = 3f;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (enabled)
        {
            grounded = true;

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

           
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (enabled)
        {
            grounded = false;
        }
    }

    public void Activate()
    {
        enabled = true;

        GetComponent<Rigidbody>().mass = 8;
        GetComponent<MeshFilter>().mesh = rhinoMesh;
    }

    public void Deactivate()
    {
        enabled = false;
    }
}
