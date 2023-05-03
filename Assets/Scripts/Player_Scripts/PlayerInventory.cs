using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script will be responsible for tracking the items the player has or has not collected, and their current stock of consumable items.
 * Items come in two forms - key item and consumable item
 * A key item can be collected once. The player either has it or does not have it (boolean)
 * A consumable item is tracked in quantities between minimum (0) and max
 * If the player picks up a consumable and it's at max, then it does not get picked up
 * If the player picks up a consumable and it's near max, the excess is lost.
 */

public class PlayerInventory : MonoBehaviour
{

    Dictionary<string, int> consumablePouch = new Dictionary<string, int>();

    //'keychain' array or list of booleans determining whether different key items are held
    //1. GreenKey
    //2. YellowKey
    //3. RedKey
    //turn into dictionary instead?
    private List<bool> oldkeyChain = new List<bool>() { false, false, false };
    Dictionary<string, bool> newKeyChain = new Dictionary<string, bool>();

    private List<KeyItem> keyChain = new List<KeyItem>();

    //singleton
    public static PlayerInventory Instance;

    [SerializeField] GameObject inventoryPanel;

    private KeyItem currentlyHeldItem;

    private void OnEnable()
    {
        consumablePouch.Add("ammo", 0);
        consumablePouch.Add("peanuts", 0);
        consumablePouch.Add("beans", 0);

        newKeyChain.Add("GreenKey", false);
        newKeyChain.Add("YellowKey", false);
        newKeyChain.Add("RedKey", false);

        keyChain.Add(new KeyItem("GreenKey", false, false));
        keyChain.Add(new KeyItem("YellowKey", false, false));
        keyChain.Add(new KeyItem("RedKey", false, false));

        currentlyHeldItem = new KeyItem("None", false, false);
    }

    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("There should only be one Player_Inventory in the scene");
        }

        //subscribe to events:
        //gain/lose a quantity of items event
        EventManager.updateConsumableItemCountEvent += UpdateResourceCount;

        //collect a key item event
        EventManager.gainKeyItemEvent += GainKeyItem;
        //check if a key item is held event
        EventManager.checkHoldingEvent += CheckKeyStatus;

        ToggleInventory(false);

    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (inventoryPanel.activeSelf == true)
            {
                ToggleInventory(false);
            }
            else
            {
                ToggleInventory(true);
            }
        }

        ItemChangeInput();
    }

    private void ItemChangeInput()
    {
        if (Input.GetButtonDown("Inventory1"))
        {
            Debug.Log("Tried to equip" + keyChain[0].CheckKeyItemID());
            ChangeEquippedItem(keyChain[0].CheckKeyItemID());
        }
        else if (Input.GetButtonDown("Inventory2"))
        {
            ChangeEquippedItem(keyChain[1].CheckKeyItemID());
            Debug.Log("Tried to equip" + keyChain[1].CheckKeyItemID());
        }
        else if (Input.GetButtonDown("Inventory3"))
        {
            ChangeEquippedItem(keyChain[2].CheckKeyItemID());
            Debug.Log("Tried to equip" + keyChain[2].CheckKeyItemID());
        }
    }

    //method for changing what the currently held item is
    private void ChangeEquippedItem(string itemID) //takes in a string which is used for identifying which item we are switching to and for checking if we actually own the item
    {
        //iterate through the list of keyItems (keychain) 
        for (int item = 0; item < keyChain.Count; item++)
        {
            //check if the itemID being passed to this method matches the items in the keychain
            if (itemID == keyChain[item].CheckKeyItemID())
            {
                //if it matches check if the item has been acquired
                if (keyChain[item].CheckIfAcquired() == true)
                {
                    //f we own the item then we set it to the currently held item after toggling the previously held item to 'not held'
                    currentlyHeldItem.SetHeld(false);
                    keyChain[item].SetHeld(true);
                    currentlyHeldItem = keyChain[item];
                    Debug.Log("Currently held item is " + currentlyHeldItem.CheckKeyItemID());

                    //update the currently held item on the UI through an event passiung it 'item'
                    EventManager.changeCurrentlyEquippedIconEvent(item);
                }
                else
                {
                    //if we don't own the item and nothing is equipped then do nothing/keep it as "none"
                    //if we are already holding something then we continue to hold that thing.
                    if (currentlyHeldItem.CheckKeyItemID() != "None") //debugs are just for testing, might add in sound based feedback/flashing UI element later if we're feeling cute idk
                    {
                        Debug.Log("You don't have that item. Continuing to hold " + currentlyHeldItem.CheckKeyItemID());
                    }
                    else
                    {
                        Debug.Log("You don't own that item. You're still holding " + currentlyHeldItem.CheckKeyItemID());
                    }
                }
            }
        }
    }

    //method for gaining or losing a quantity of a particular consumable    
    private void UpdateResourceCount(string resourceID, int amount)
    {
        consumablePouch[resourceID] += amount;
        Debug.Log("You now have " + consumablePouch[resourceID] + " of " + resourceID);
    }


    //method for collect a key item
    //takes in an item ID value and toggles the matching boolean in the 'keychain' array to true
    private void GainKeyItem(string keyID)
    {
        for (int key = 0; key < keyChain.Count; key++)
        {
            if (keyID == keyChain[key].CheckKeyItemID())
            {
                keyChain[key].SetAcquired(true);
                EventManager.toggleKeyItemIconEvent(true, keyChain[key].CheckKeyItemID());
            }
        }

        #region old keychain code
        /*
        oldkeyChain[(int)keyID] = true;
        Debug.Log("Setting " + keyID + ", which was false, to " + oldkeyChain[keyID]);

        //placeholder until I replace the list of booleans with a dictionary top be waaaay cooler
        switch (keyID)
        {
            case 0:
                //invoke toggleKeyItemIcon event with GreenKey string
                EventManager.toggleKeyItemIconEvent(true, "GreenKey");
                break;
            case 1:
                //invoke same with RedKey string
                EventManager.toggleKeyItemIconEvent(true, "RedKey");
                break;
            case 2:
                //invvooooke
                EventManager.toggleKeyItemIconEvent(true, "YellowKey");
                break;
        }*/
        #endregion

    }

    //method for checking if a key item is held
    private bool CheckKeyStatus(int keyID)
    {
        return keyChain[keyID].CheckIfHeld();
    }

    private void OnDestroy()
    {
        //collect a key item event
        EventManager.gainKeyItemEvent -= GainKeyItem;
        //check if a key item is held event
        EventManager.checkHoldingEvent -= CheckKeyStatus;

        EventManager.updateConsumableItemCountEvent -= UpdateResourceCount;
    }

    public void ToggleInventory(bool openClose)
    {
        inventoryPanel.SetActive(openClose);
        EventManager.playerCanMoveEvent(!openClose);
    }

}
