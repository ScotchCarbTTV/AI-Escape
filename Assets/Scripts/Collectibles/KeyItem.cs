using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    private string keyItemID;

    private bool keyItemAcuired;

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
    }
}
