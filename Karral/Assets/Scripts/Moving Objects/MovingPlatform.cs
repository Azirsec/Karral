using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] Vector3 movementSpeed;
    [SerializeField] float maxMoveTime;

    float currentTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed.z = 0;
        currentTime = maxMoveTime / 2;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;
        GetComponent<Rigidbody>().velocity = Vector3.ClampMagnitude(GetComponent<Rigidbody>().velocity + movementSpeed, movementSpeed.magnitude);

        if (currentTime > maxMoveTime)
        {
            currentTime = 0;
            flipDirection();
        }
    }

    void flipDirection()
    {
        movementSpeed -= movementSpeed * 2;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerHuman>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(movementSpeed / 10, ForceMode.VelocityChange);
        }
    }
}
