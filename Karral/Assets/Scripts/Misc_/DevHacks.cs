using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevHacks : MonoBehaviour
{
    public void Gorilla(bool unlock)
    {
        GlobalStorage.gorillaDNA = unlock;
    }

    public void Mouse(bool unlock)
    {
        GlobalStorage.mouseDNA = unlock;
    }

    public void Rhino(bool unlock)
    {
        GlobalStorage.rhinoDNA = unlock;
    }

    public void Eagle(bool unlock)
    {
        GlobalStorage.eagleDNA = unlock;
    }
}
