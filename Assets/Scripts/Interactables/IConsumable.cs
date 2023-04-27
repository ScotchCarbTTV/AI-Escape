using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IConsumable : MonoBehaviour, IInteraction
{
    public enum ConsumableID { ammo, beans, peanuts }
    public ConsumableID consumableID;

    //the identity of this consumable (ammo, peanuts, beans)
    string consumableIDString;

    //integer for how much of this resource should be given to the player :)
    [SerializeField] int consumableAmount;

    bool playerProx;

    private void OnEnable()
    {
        switch (consumableID)
        {
            case ConsumableID.ammo:
                consumableIDString = "ammo";
                break;
            case ConsumableID.beans:
                consumableIDString = "beans";
                break;
            case ConsumableID.peanuts:
                consumableIDString = "peanuts";
                break;
        }
    }

    public void Activate()
    {
        //check if player in range to activate hurr duur
        if (CheckAvail())
        {
            //if in range then invoke the gain X amount of thingy
            EventManager.updateConsumableItemCountEvent(consumableIDString, consumableAmount);

            //add this item back into the ammo pickup item pooler

            gameObject.SetActive(false);
        }
        else
        {
            //display info tag
        }

    }

    public bool CheckAvail()
    {
        return playerProx;
    }

    public void ToggleInteract(bool toggle)
    {
        playerProx = toggle;
    }
}
