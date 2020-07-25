using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEagle : MonoBehaviour
{
    float moveDir;
    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;
    [SerializeField] int totalJumps;

    int currentJumps;

    [SerializeField] Mesh eagleMesh;

    float jumptimer = 0f;

    bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        GetComponent<BasicMovement>().basicMovement(maxSpeed, accelerationDuration, decelerationDuration);
        Jump();
    }

    private void Jump()
    {
        jumptimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            
            if (jumptimer <= 0f && currentJumps > 0)
            {
                if (!grounded)
                {
                    currentJumps--;
                }

                GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpSpeed, 0);
                jumptimer = 0.3f;
                grounded = false;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (enabled)
        {
            for (int i = 0; i < collision.contactCount; i++)
            {
                if (collision.contacts[i].normal.y >= 0.9f)
                {
                    grounded = true;
                    currentJumps = totalJumps;
                }
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

        GetComponent<Rigidbody>().mass = 0.75f;
        GetComponent<MeshFilter>().mesh = eagleMesh;
        GetComponent<CapsuleCollider>().height = 1.5f;
    }

    public void Deactivate()
    {
        enabled = false;
    }
}
