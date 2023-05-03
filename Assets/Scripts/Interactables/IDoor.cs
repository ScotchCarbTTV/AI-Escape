using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IDoor : MonoBehaviour, IInteraction
{
    private bool playerProx = false;

    private MeshRenderer meshRenderer;

    IInteraction interactable;

    //variable for accessing the animator component
    private Animator doorAnimC;

    private void Start()
    {
        doorAnimC = GetComponent<Animator>();

        interactable = this;

        meshRenderer = GetComponentInChildren<MeshRenderer>();
        meshRenderer.material.color = Color.red;
    }

    public void Activate()
    {
        //open the door

        if (CheckAvail() == true)
        {
            if (doorAnimC.GetBool("Open"))
            {
                doorAnimC.SetBool("Open", false);
            }
            else
            {
                doorAnimC.SetBool("Open", true);
            }
        }
        else
        {
            //invoke the 'toggleInteractableInfoEvent', passing it this proximitydetector script and 'true'
            EventManager.toggleInteractableInfoDisplayEvent(interactable, true);
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
