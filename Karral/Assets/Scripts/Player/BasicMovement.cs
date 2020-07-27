using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    string currentAnimation;
    public string basicMovement(float maxSpeed, float accelerationDuration, float decelerationDuration, bool grounded)
    {
        float movedir = Input.GetAxisRaw("Horizontal");

        //trying to move right
        if (movedir > 0.15f)
        {
            // if already moving left, decelerate
            if (GetComponent<Rigidbody>().velocity.x < 0)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(maxSpeed / decelerationDuration * Time.deltaTime, 0, 0);
                currentAnimation = "Left Stop";
            }
            //if not moving left, accelerate
            else if (GetComponent<Rigidbody>().velocity.x < maxSpeed)
            {
                GetComponent<Rigidbody>().velocity += new Vector3(maxSpeed / accelerationDuration * Time.deltaTime, 0, 0);
                currentAnimation = "Right Accelerate";
            }
        }
        //trying to move left
        else if (movedir < -0.15f)
        {
            // already moving right, decelerate
            if (GetComponent<Rigidbody>().velocity.x > 0)
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(maxSpeed / decelerationDuration * Time.deltaTime, 0, 0);
                currentAnimation = "Right Stop";
            }
            // already moving left, accelerate
            else if (GetComponent<Rigidbody>().velocity.x > -maxSpeed)
            {
                GetComponent<Rigidbody>().velocity -= new Vector3(maxSpeed / accelerationDuration * Time.deltaTime, 0, 0);
                currentAnimation = "Left Accelerate";
            }
        }
        // not trying to move in any direction
        else
        {
            // if already slow, set velocity to 0
            if (Mathf.Abs(GetComponent<Rigidbody>().velocity.x) < (maxSpeed / decelerationDuration * Time.deltaTime) * 2)
            {
                GetComponent<Rigidbody>().velocity = new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
                currentAnimation = "Idle";
            }
            else 
            {
                if (GetComponent<Rigidbody>().velocity.x > 0)
                {
                    currentAnimation = "Right Stop";
                }
                else
                {
                    currentAnimation = "Left Stop";
                }
                // if not slow, slow down
                if (Time.deltaTime != 0)
                {
                    if (grounded)
                    {
                        GetComponent<Rigidbody>().velocity -= new Vector3(
                            GetComponent<Rigidbody>().velocity.x / Mathf.Abs(GetComponent<Rigidbody>().velocity.x) *
                            maxSpeed / decelerationDuration * Time.deltaTime,
                            0,
                            0);
                    }
                    else
                    {
                        GetComponent<Rigidbody>().velocity -= new Vector3(
                            GetComponent<Rigidbody>().velocity.x / Mathf.Abs(GetComponent<Rigidbody>().velocity.x) *
                            maxSpeed / accelerationDuration * Time.deltaTime * 0.2f,
                            0,
                            0);
                    }
                }
            }
         
        }
        return currentAnimation;
    }
}
