using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEagle : MonoBehaviour
{
    float moveDir;

    [SerializeField] GameObject mesh;

    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;
    [SerializeField] int totalJumps;

    [SerializeField] float weight;
    [SerializeField] float height;
    [SerializeField] float width;

    [SerializeField] GameObject featherUI;
    [SerializeField] GameObject[] feathers =  new GameObject[4];

    int currentJumps;

    float jumptimer = 0f;

    bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        GetComponent<BasicMovement>().basicMovement(maxSpeed, accelerationDuration, decelerationDuration, grounded);
        Jump();
        updateFeathers();
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
                feathers[3].SetActive(false);
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            if (collision.contacts[i].normal.y >= 0.9f)
            {
                grounded = true;
                currentJumps = totalJumps;
                feathers[3].SetActive(true);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (enabled)
        {
            grounded = false;
            feathers[3].SetActive(false);
        }
    }

    void updateFeathers()
    {
        for (int i = 0; i < 3; i++)
        {
            if (i < currentJumps)
            {
                feathers[i].SetActive(true);
            }
            else
            {
                feathers[i].SetActive(false);
            }
        }
    }

    public void Activate()
    {
        enabled = true;

        featherUI.SetActive(true);

        // set eagle values
        GetComponent<Rigidbody>().mass = weight;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<CapsuleCollider>().radius = width / 2f;
        GetComponent<CapsuleCollider>().height = height;
        GetComponent<CapsuleCollider>().direction = 1;

        mesh.SetActive(true);
    }

    public void Deactivate()
    {
        enabled = false;
        featherUI.SetActive(false);
        mesh.SetActive(false);
    }
}
