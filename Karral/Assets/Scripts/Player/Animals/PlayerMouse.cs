using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouse : MonoBehaviour
{
    float movedir;

    [SerializeField] GameObject mesh;
    [SerializeField] Animator animator;

    [SerializeField] float maxSpeed;
    [SerializeField] float accelerationDuration;
    [SerializeField] float decelerationDuration;
    [SerializeField] float jumpSpeed;

    [SerializeField] float weight;
    [SerializeField] float height;
    [SerializeField] float width;

    [SerializeField] bool wallwalking = false;

    float jumptimer = 0.1f;

    float nonZeroTempX = 0;

    [SerializeField] bool grounded = false;
    [SerializeField] bool pushing = false;

    // Update is called once per frame
    void Update()
    {
        //print(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
        print(Mathf.Abs(transform.GetComponent<Rigidbody>().velocity.x));

        float tempX = Input.GetAxisRaw("Horizontal");
        
        if (tempX > 0)
        {
            nonZeroTempX = tempX;
            mesh.transform.eulerAngles = new Vector3(mesh.transform.eulerAngles.x, 90, mesh.transform.eulerAngles.z);
        }
        else if (tempX < 0)
        {
            nonZeroTempX = tempX;
            mesh.transform.eulerAngles = new Vector3(mesh.transform.eulerAngles.x, -90, mesh.transform.eulerAngles.z);
        }
        
        GetComponent<BasicMovement>().basicMovement(maxSpeed, accelerationDuration, decelerationDuration, grounded);

        if (wallwalking)
        {
            float tempY = Input.GetAxisRaw("Vertical");
            if (tempY > 0)
            {
                mesh.transform.eulerAngles = new Vector3(-90, 90 * nonZeroTempX, mesh.transform.eulerAngles.z);
            }
            else if (tempY < 0)
            {
                mesh.transform.eulerAngles = new Vector3(90, -90 * nonZeroTempX, mesh.transform.eulerAngles.z);
            }
            wallMovement();
        }
        else
        {
            mesh.transform.eulerAngles = new Vector3(0, mesh.transform.eulerAngles.y, mesh.transform.eulerAngles.z);
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

    private void LateUpdate()
    {
        animationStuff();
    }

    void animationStuff()
    {
        animator.SetBool("isGrounded", grounded);
        animator.SetBool("isPushing", pushing);
        animator.SetFloat("xVel", Mathf.Abs(transform.GetComponent<Rigidbody>().velocity.x));
        animator.SetFloat("yVel", transform.GetComponent<Rigidbody>().velocity.y);

        //animator.SetLayerWeight(1, 1);
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
                if (Mathf.Abs(collision.contacts[i].normal.x) > 0.9 && collision.gameObject.tag == "Box")
                {
                    pushing = true;
                    GetComponent<Rigidbody>().useGravity = false;
                }
                if(Mathf.Abs(collision.contacts[i].normal.x) > 0.9)
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
        pushing = false;
    }

    public void Activate()
    {
        enabled = true;

        // set mouse values
        GetComponent<Rigidbody>().mass = weight;
        GetComponent<CapsuleCollider>().enabled = true;
        GetComponent<CapsuleCollider>().radius = width / 2f;
        GetComponent<CapsuleCollider>().height = height;
        GetComponent<CapsuleCollider>().direction = 1;

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
