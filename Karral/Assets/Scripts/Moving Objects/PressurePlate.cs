using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    float startHeight;

    // Start is called before the first frame update
    void Start()
    {
        startHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > startHeight)
        {
            transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);
            GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Force);
    }
}
