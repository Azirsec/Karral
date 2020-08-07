﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRhino : MonoBehaviour
{
    float movedir;
    int faceDirection;

    [SerializeField] GameObject mesh;

    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;

    [SerializeField] float weight;
    [SerializeField] float height;
    [SerializeField] float width;

    float jumptimer = 0.1f;

    bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        GetComponent<BasicMovement>().basicMovement(maxSpeed, accelerationDuration, decelerationDuration, grounded);
        Jump();
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

    public void Activate()
    {
        enabled = true;

        GetComponent<Rigidbody>().mass = weight;
        GetComponent<BoxCollider>().size = new Vector3(width, height, 1);
        mesh.SetActive(true);
    }

    public void Deactivate()
    {
        enabled = false;
        mesh.SetActive(false);
    }
}