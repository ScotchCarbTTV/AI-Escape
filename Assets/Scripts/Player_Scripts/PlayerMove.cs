using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Player movement script - player will use mouse and right click on ground in environment. Player character will then walk to that point (if possible).
 * This script will handle the input from the player, regsitering when they click and checking if the area under the cursor is valid.
 * If it's valid then the 'move to point' event will be called via event manager & player character will move
 */

public class PlayerMove : MonoBehaviour
{
    //boolean for 'is player allowed to move'
    private bool strobeVII = true;

    void Start()
    {
        //subscribe to events?
        EventManager.playerCanMoveEvent += TogglePlayerCanMove;
    }

    void Update()
    {
        //if player is allowed to move
        if (strobeVII == true)
        {
            //check for player right click
            if (Input.GetMouseButtonDown(1))
            {
                MovePlayer();
            }
        }

    }

    //method for checking where the mouse is when player right clicks
    private void MovePlayer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        int lMask = 1 << 2;
        lMask = ~lMask;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, lMask))
        {
            //check if the mouse was over a valid spot to move to
            if (hit.collider.tag == "Ground")
            {
                //Debug.Log("Mouse click detected. Pointing at ground.");
                //invoke the required event                
                EventManager.updateNavPositionEvent(hit.point);             

            }
            else if(hit.collider.TryGetComponent(out IInteraction interactable))
            {
                Vector3 lolPos = hit.collider.transform.position - transform.position;
                lolPos = lolPos.normalized * 2;
                //Vector3 newNavPoint = hit.collider.transform.position - lolPos;
                EventManager.updateNavPositionEvent(hit.point - lolPos);                
            }
            else
            {
                //Debug.Log("Mouse click detected. Thumbcut gay.");
            }

        }
        else
        {
            //Debug.Log("Mouse click detected. No target found.");
        }
    }

    //method for toggling allowed to move & not allowed to move
    private void TogglePlayerCanMove(bool canMove)
    {
       
        strobeVII = canMove;
        //Debug.Log(strobeVII);
    }

    private void OnDestroy()
    {
        EventManager.playerCanMoveEvent -= TogglePlayerCanMove;
    }
}
