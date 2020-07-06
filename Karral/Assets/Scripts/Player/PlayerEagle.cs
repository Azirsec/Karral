using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEagle : MonoBehaviour
{
    float moveDir;
    [SerializeField] float speed;
    [SerializeField] float flyForce;
    [SerializeField] float rechargeLength;
    [SerializeField] float flapDuration;

    [SerializeField] Mesh eagleMesh;

    bool flapping = false; // if yes, then bird is in flapping state and will gain upward force

    float flapTimer = 0; //counts up until exceeding flap duration, bird will go up during this

    int maxFlightCharges = 3; // cannot store more than 3 flap charges
    int currFlightCharges = 3; // if current charges is 0 you can't flap

    float rechargeTimer = 0; // counts up until recharge time, once it exceeds it player regains one flight charge

    bool grounded = false;

    // Update is called once per frame
    void Update()
    {
        basicMovement();
        flight();
    }

    void basicMovement()
    {

        moveDir = Input.GetAxisRaw("Horizontal");

        GetComponent<Rigidbody>().AddForce(new Vector3(moveDir * speed, 0, 0), ForceMode.Force);

        if (grounded)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.9f, GetComponent<Rigidbody>().velocity.y, 0);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x * 0.98f, GetComponent<Rigidbody>().velocity.y, 0);

        }

        if (Input.GetKey(KeyCode.Space))
        {
            beginFlight();
        }
    }

    void flight()
    {
        if (flapping)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, flyForce, 0), ForceMode.Force);

            flapTimer += Time.deltaTime;

            if (flapTimer > flapDuration)
            {
                flapTimer = 0;
                flapping = false;
            }
        }

        if (currFlightCharges < maxFlightCharges)
        {
            rechargeTimer += Time.deltaTime;
            if (rechargeTimer > rechargeLength)
            {
                rechargeTimer = 0;
                currFlightCharges += 1;
            }
        }
    }

    private void beginFlight()
    {
        if (flapTimer <= 0 && currFlightCharges > 0)
        {
            currFlightCharges -= 1;
            flapping = true;
        }
    }


    private void OnCollisionStay(Collision collision)
    {
        if (enabled)
        {
            grounded = true;
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

        GetComponent<Rigidbody>().mass = 0.5f;
        GetComponent<MeshFilter>().mesh = eagleMesh;
    }

    public void Deactivate()
    {
        enabled = false;
    }
}
