using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    public HubStorage load;

    // Start is called before the first frame update
    void Start()
    {
        load.loadGame();
    }
}
