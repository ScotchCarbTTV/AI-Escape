using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ILockedDoor : MonoBehaviour, IInteraction
{

    private bool playerProx = false;

    private MeshRenderer meshRenderer;

    IInteraction interactable;

    private void Start()
    {
        interactable = this;

        meshRenderer = GetComponentInParent<MeshRenderer>();
        meshRenderer.material.color = Color.red;
    }

    public enum KeyID { GreenKey, YellowKey, RedKey };
    public KeyID keyID;

    public void Activate() 
    {        

        //open the door
        //placeholder PLAHCEOLDER WOOT WOOT
        //NEED TO HAVE THE 'PLAYERACTIVEITEM' SCRIPT TO CHECK IF WE'RE HOLDING THE RIGHT KEY, NOT JUST IF WE OWN IT
        if (EventManager.checkHoldingEvent((int)keyID))
        {

            if (CheckAvail() == true)
            {
                Debug.Log("The door creaks open");
            }
            else
            {
                //invoke the 'toggleInteractableInfoEvent', passing it this proximitydetector script and 'true'
                EventManager.toggleInteractableInfoDisplayEvent(interactable, true);
            }
        }
        else
        {
            Debug.Log("You need the " + keyID + " item to do that!");
        }

    }

    public void ToggleInteract(bool toggle)
    {
        playerProx = toggle;
        if (toggle == true)
        {
            meshRenderer.material.color = Color.blue;
        }
        else
        {
            meshRenderer.material.color = Color.red;
        }
    }

    public bool CheckAvail()
    {
        return playerProx;
    }

    /*
     * Note: could potentially use these to detect mousing over for the purpose of setting the object to the 'selected' interaction rather than waiting for click & raycast...
     * 
    private void OnMouseEnter()
    {
        Debug.Log("Mouse is now pointed at this");
    }
    */
    private void OnMouseExit()
    {
        // Debug.Log("Mouse is no longer pointed at this");
        //invoke the 'toggleInteractableInfoEvent', passing it this proximitydetector script and 'false'
        EventManager.toggleInteractableInfoDisplayEvent(interactable, false);

    }
}
