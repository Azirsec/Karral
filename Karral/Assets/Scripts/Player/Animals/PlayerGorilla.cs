using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGorilla : MonoBehaviour
{
    int faceDirection = 1;

    [SerializeField] GameObject mesh;
    [SerializeField] Animator animator;

    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;

    [SerializeField] float throwVelocity;

    [SerializeField] float weight;
    [SerializeField] float height;
    [SerializeField] float width;

    GameObject heldBox;

    float throwTimer = 0.3f;

    float jumptimer = 0.1f;

    bool grounded = false;
    bool pushing = false;
    bool carrying = false;


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
            mesh.transform.eulerAngles = new Vector3(mesh.transform.eulerAngles.x, 90, mesh.transform.eulerAngles.z);
        }
        else if (temp < 0)
        {
            faceDirection = -1;
            mesh.transform.eulerAngles = new Vector3(mesh.transform.eulerAngles.x, -90, mesh.transform.eulerAngles.z);
        }

        GetComponent<BasicMovement>().basicMovement(maxSpeed, accelerationDuration, decelerationDuration, grounded);
    }

    private void LateUpdate()
    {
        animationStuff();
    }

    void animationStuff()
    {
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Pushing", pushing);
        animator.SetBool("Carrying", carrying);
        animator.SetFloat("XSpeed", Mathf.Abs(transform.GetComponent<Rigidbody>().velocity.x));
        animator.SetFloat("YSpeed", transform.GetComponent<Rigidbody>().velocity.y);

        animator.SetLayerWeight(1, 1);
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
            carrying = true;

            heldBox.GetComponent<BoxCollider>().enabled = false;

            heldBox.transform.position = transform.position + Vector3.up * 1.5f + new Vector3(0, heldBox.transform.localScale.y / 2f, 0);
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
        throwTimer = 0.3f;
        heldBox.GetComponent<BoxCollider>().enabled = true;

        carrying = false;

        heldBox = null;
    }

    private void dropBox()
    {
        if (heldBox != null)
        {
            heldBox.GetComponent<BoxCollider>().enabled = true;
        }

        carrying = false;

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
                if (Mathf.Abs(collision.contacts[i].normal.x) > 0.8 && !(collision.contacts[i].otherCollider.gameObject == heldBox))
                {
                    pushing = true;
                }
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        grounded = false;
        pushing = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (enabled)
        {
            if (other.GetComponent<Box>() != null && heldBox == null)
            {
                if (Input.GetKey(KeyCode.E) && throwTimer < 0)
                {
                    throwTimer = 0.05f;
                    heldBox = other.gameObject;
                    pushing = false;
                }
            }
        }
    }

    public void Activate()
    {
        enabled = true;

        // set gorilla values
        GetComponent<Rigidbody>().mass = weight;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<CapsuleCollider>().radius = width / 2f;
        GetComponent<CapsuleCollider>().height = height;
        GetComponent<CapsuleCollider>().direction = 1;

        mesh.SetActive(true);
    }

    public void Deactivate()
    {
        dropBox();
        enabled = false;
        throwTimer = 0.1f;
        jumptimer = 0.1f;
        mesh.SetActive(false);
    }
}
