using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    //string ID for the key item gets used when checked which item this is
    private string keyItemID;

    //bool variable for checking if this key item is acquired
    private bool keyItemAcuired;

    //bool variable for cehcking if the player is currently 'holding' this key item
    private bool keyItemHeld;

    internal KeyItem(string _keyItemID, bool _keyItemAcquired, bool _keyItemHeld)
    {
        keyItemID = _keyItemID;
        keyItemAcuired = _keyItemAcquired;
        keyItemHeld = _keyItemHeld;
    }

    public bool CheckIfAcquired()
    {
        return keyItemAcuired;
    }

    public bool CheckIfHeld()
    {
        return keyItemHeld;
    }

    public string CheckKeyItemID()
    {
        return keyItemID;
    }

    public void SetAcquired(bool _acquired)
    {
        keyItemAcuired = _acquired;
    }

    public void SetHeld(bool _held)
    {
        keyItemHeld = _held;
        //toggle the UI image to show which item is currently held
    }
}
