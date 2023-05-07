using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Script for the different consumables which can exist
 * Is used in the ConsumablePouch class, and takes the information from the IConsumable objects to determine:
 * stringID
 * maximum consumable allowed
 * how much of consumable player starts with
 *
 */

public class Consumable : MonoBehaviour
{
    private string consumableID;
    public string ConsumableID { get { return consumableID; } }

    private int consumableMax;
    public int ConsumableMax { get { return consumableMax; } }

    private int consumableStartingAmount;
    public int ConsumableStartingAmount { get { return consumableStartingAmount; } }

    private int consumableCurrentCount;
    public int ConsumableCurrentCount { get { return consumableCurrentCount; } }

    internal Consumable (string _ID, int _conMax, int _startingCount)
    {
        consumableID = _ID;
        consumableMax = _conMax;
        consumableStartingAmount = _startingCount;
        consumableCurrentCount = _startingCount;
    }

    public void UpdateConsumableMax(int _newMax)
    {
        consumableMax = _newMax;
    }
}
