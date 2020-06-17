using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyColour
{
    red,
    blue,
    yellow,
    green
}

public class Key : MonoBehaviour
{
    [SerializeField] KeyColour colour;

    public KeyColour getColour()
    {
        return colour;
    }
}
