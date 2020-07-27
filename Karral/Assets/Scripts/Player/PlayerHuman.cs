using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHuman : MonoBehaviour
{
    [SerializeField] GameObject mesh;

    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;

    List<KeyColour> heldKeys = new List<KeyColour>();

    float jumptimer = 0.1f;

    bool grounded = false;

    private void Start()
    {
        //Application.targetFrameRate = 60;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<BasicMovement>().basicMovement(maxSpeed, accelerationDuration, decelerationDuration, grounded);
        jumptimer -= Time.deltaTime;
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
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
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
            grounded = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (enabled)
        {
            //open and close door
            if (other.GetComponent<Door>() != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
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
                //Destroy(other.gameObject);
            }

            //unlock door
            if (other.GetComponent<Door>() != null)
            {
                other.GetComponent<Door>().unlock(heldKeys);
            }
        }
    }

    public void Activate()
    {
        enabled = true;

        GetComponent<Rigidbody>().mass = 1;
        GetComponent<CapsuleCollider>().height = 2;
        mesh.SetActive(true);
    }

    public void Deactivate()
    {
        enabled = false;
        jumptimer = 0.1f;
        mesh.SetActive(false);
    }
}
