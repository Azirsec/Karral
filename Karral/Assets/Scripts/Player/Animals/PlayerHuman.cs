using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHuman : MonoBehaviour
{
    [SerializeField] GameObject mesh;
    [SerializeField] Animator animator;

    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;

    [SerializeField] float weight;
    [SerializeField] float height;
    [SerializeField] float width;

    List<KeyColour> heldKeys = new List<KeyColour>();

    [SerializeField] Image rKey;
    [SerializeField] Image gKey;
    [SerializeField] Image bKey;

    float jumptimer = 0.1f;

    int faceDirection = 1;
    bool pushing = false;

    bool grounded = false;

    bool firstframe = true;

    private void Start()
    {
        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        if (!firstframe)
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

            jumptimer -= Time.deltaTime;
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }
        }
        else
        {
            firstframe = false;
        }
    }

    private void LateUpdate()
    {
        animationStuff();
    }

    void animationStuff()
    {
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Pushing", pushing);
        animator.SetFloat("XSpeed", Mathf.Abs(transform.GetComponent<Rigidbody>().velocity.x));
        animator.SetFloat("YSpeed", transform.GetComponent<Rigidbody>().velocity.y);
    }

    private void Jump()
    {
        if (grounded)
        {
            if (jumptimer <= 0f)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(0, jumpSpeed, 0);
                jumptimer = 0.1f;
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
                if (Mathf.Abs(collision.contacts[i].normal.x) > 0.8)
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
            //open and close door
            if (other.GetComponent<Door>() != null)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    other.GetComponent<Door>().interact();
                }
            }

            //pick up key
            if (other.GetComponent<Key>() != null)
            {
                heldKeys.Add(other.GetComponent<Key>().getColour());
                other.GetComponentsInChildren<MeshRenderer>()[1].enabled = false;
                other.GetComponent<SphereCollider>().enabled = false;
                updateKeyUI();
            }

            //unlock door
            if (other.GetComponent<Door>() != null)
            {
                other.GetComponent<Door>().unlock(heldKeys);
                updateKeyUI();
            }
        }
    }

    void disableKeys()
    {
        rKey.enabled = false;
        gKey.enabled = false;
        bKey.enabled = false;
    }

    void updateKeyUI()
    {
        disableKeys();

        foreach (KeyColour key in heldKeys)
        {
            switch (key)
            {
                case KeyColour.red:
                    rKey.enabled = true;
                    break;

                case KeyColour.green:
                    gKey.enabled = true;
                    break;

                case KeyColour.blue:
                    bKey.enabled = true;
                    break;
            }
        }
    }

    public void Activate()
    {
        enabled = true;

        // set human values
        GetComponent<Rigidbody>().mass = weight;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<CapsuleCollider>().radius = width / 2f;
        GetComponent<CapsuleCollider>().height = height;
        GetComponent<CapsuleCollider>().direction = 1;

        mesh.SetActive(true);
        //updateKeyUI();
    }

    public void Deactivate()
    {
        enabled = false;
        jumptimer = 0.1f;
        mesh.SetActive(false);

        //disableKeys();
    }
}
