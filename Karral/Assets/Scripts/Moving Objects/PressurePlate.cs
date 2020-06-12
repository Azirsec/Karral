using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] GameObject plate;
    [SerializeField] GameObject linkedObject;
    [SerializeField] bool showObjectWhenPressed;

    float startHeight;

    // Start is called before the first frame update
    void Start()
    {
        startHeight = plate.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (plate.transform.position.y > startHeight)
        {
            plate.transform.position = new Vector3(plate.transform.position.x, startHeight, plate.transform.position.z);
            plate.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        plate.GetComponent<Rigidbody>().AddForce(Vector3.up, ForceMode.Force);

        if (plate.transform.position.y < startHeight - 0.05f)
        {
            linkedObject.SetActive(showObjectWhenPressed);
        }
        else
        {
            linkedObject.SetActive(!showObjectWhenPressed);
        }
    }
}
