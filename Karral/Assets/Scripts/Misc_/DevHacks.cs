using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevHacks : MonoBehaviour
{
    public void Gorilla(bool unlock)
    {
        HubStorage.gorillaDNA = unlock;
    }

    public void Mouse(bool unlock)
    {
        HubStorage.mouseDNA = unlock;
    }

    public void Rhino(bool unlock)
    {
        HubStorage.rhinoDNA = unlock;
    }

    public void Eagle(bool unlock)
    {
        HubStorage.eagleDNA = unlock;
    }
}
