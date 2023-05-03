using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{

    //singleton variable for this object
    public static EventManager Instance;



    private void Awake()
    {
        if (Instance == null)
        {
            //Debug.Log("Assigned the eventmanager instance succesfully.");
            Instance = this;
        }
        else
        {
            Debug.LogError("There should only be one EventManager script in the scene!");
        }
    }

    void Start()
    {

    }

    //events and delegates

    //player can move event
    public delegate void PlayerCanMove(bool canMove);
    public static PlayerCanMove playerCanMoveEvent;


    //update the player nav object position event
    public delegate void UpdateNavPosition(Vector3 newPos);
    public static UpdateNavPosition updateNavPositionEvent;

    //event for when player left ctrl + left clicks interactable item
    public delegate void LeftCtrlClickInteraction(GameObject interaction);
    public static LeftCtrlClickInteraction leftCtrlClickInteractionEvent;

    //event for toggling interactable info displays on and off
    public delegate void ToggleInteractableInfoDisplay(IInteraction interactable, bool offOn);
    public static ToggleInteractableInfoDisplay toggleInteractableInfoDisplayEvent;

    //event for gaining a key item
    public delegate void GainKeyItem(string keyID);
    public static GainKeyItem gainKeyItemEvent;

    //event for checking if the player owns and is holding a particular item
    public delegate bool CheckHolding(int keyID);
    public static CheckHolding checkHoldingEvent;

    //event for gaining x amount of the specific item
    public delegate void UpdateConsumableItemCount(string consumableID, int amount);
    public static UpdateConsumableItemCount updateConsumableItemCountEvent;

    public delegate void ToggleKeyItemIcon(bool onOff, string keyIconID);
    public static ToggleKeyItemIcon toggleKeyItemIconEvent;

    //event for changing the UI icon display for the currently held item
    public delegate void ChangeCurrentlyEquippedItemIcon(int itemID);
    public static ChangeCurrentlyEquippedItemIcon changeCurrentlyEquippedIconEvent;

}
