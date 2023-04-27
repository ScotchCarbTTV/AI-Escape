using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] float interactionDistance;

    [SerializeField] GameObject player;

    private bool canInteract = true;

    private void Start()
    {
        EventManager.playerCanMoveEvent += ToggleCanInteract;
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //throw a raycast at the fucking GROOOUUUND
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                RaycastHit hit;

                int lMask = 1 << 2;
                lMask = ~lMask;

                if (Physics.Raycast(ray, out hit, Mathf.Infinity, lMask))
                {
                    //check if the mouse was over a valid spot to move to
                    if (hit.collider.TryGetComponent(out IInteraction interaction))
                    {
                        //Debug.Log("Mouse click detected. Pointing at interactable object.");

                        //check if the player character is within interaction range
                        //THIS SHOULD USE THE 'CAN BE INTERACTED WITH' BOOL ON THE THING INSTEAD

                        //get player character to face towards the thing interacting with

                        //trigger interaction on the object
                        if (!Input.GetKey(KeyCode.LeftControl))
                        {
                            interaction.Activate();
                        }



                    }
                    else
                    {
                        //Debug.Log("Mouse click detected. No interaction found.");
                    }

                }
                else
                {
                    //Debug.Log("Mouse click detected. No target found.");
                }
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                if (Input.GetMouseButtonDown(0))
                {
                    //Debug.Log("Buh?");
                    //throw a raycast at the fucking GROOOUUUND
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                    RaycastHit hit;

                    int lMask = 1 << 2;
                    lMask = ~lMask;

                    if (Physics.Raycast(ray, out hit, Mathf.Infinity, lMask))
                    {
                        //check if the mouse was over a valid spot to move to
                        if (hit.collider.TryGetComponent(out IInteraction interaction))
                        {
                            //Debug.Log("Mouse click detected. Pointing at interactable object.");

                            //check if the player character is within interaction range
                            //THIS SHOULD USE THE 'CAN BE INTERACTED WITH' BOOL ON THE THING INSTEAD

                            if (Vector3.Distance(player.transform.position, hit.collider.transform.position) < interactionDistance)
                            {
                                //get player character to face towards the thing interacting with

                                //trigger interaction on the object
                                interaction.Activate();
                            }
                            else
                            {
                                //Debug.Log("Switching to proximity??");
                                //invoke event which will cause player to move towards interactable and activate it
                                EventManager.leftCtrlClickInteractionEvent(hit.collider.gameObject);
                            }


                        }
                        else
                        {
                            //Debug.Log("Mouse click detected. No interaction found.");
                        }
                    }
                }
            }
        }
    }

    private void ToggleCanInteract(bool _canInteract)
    {
        canInteract = _canInteract;
    }

    private void OnDestroy()
    {
        EventManager.playerCanMoveEvent += ToggleCanInteract;
    }
}


    public interface IInteraction
    {    

        void Activate();
        void ToggleInteract(bool toggle);

        bool CheckAvail();
    }
