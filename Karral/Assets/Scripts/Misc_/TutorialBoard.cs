using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialBoard : MonoBehaviour
{
    [SerializeField] SpriteRenderer tut_board;

    [SerializeField] float activateDistance;
    [SerializeField] float deactivateDistance;

    [SerializeField] GameObject player;

    bool isActive = true;
    float distanceFromPlayer;

    // Update is called once per frame
    void Update()
    {
        distanceFromPlayer = (player.transform.position - transform.position).magnitude;

        // when player approaches the board, activate
        if (distanceFromPlayer < activateDistance)
        {
            activate();
        }
        // when player goes away from the board, deactivate
        else if (distanceFromPlayer > deactivateDistance)
        {
            deactivate();
        }
    }

    // makes the board visible
    void activate()
    {
        if (!isActive)
        {
            isActive = true;
            tut_board.enabled = true;
        }
    }

    // hides the board
    void deactivate()
    {
        if (isActive)
        {
            isActive = false;
            tut_board.enabled = false;
        }
    }
}
