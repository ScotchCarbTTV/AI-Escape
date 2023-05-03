using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Displays an icon to show what the player is currently holding.
 * Script may be expanded to actually handle all of the HUD elements eg:
 * How much ammo
 * Health
 * Sanity
 * Bladder
 * Current objective
 * Minimap (if any)
 * Other shit
 */

public class EquippedItemIcon : MonoBehaviour
{
    //List of images to display according to the currently held item
    [SerializeField] private List<Sprite> heldIcons = new List<Sprite>();

    private Image currentlyHeldIcon;

    private void Start()
    {
        currentlyHeldIcon = GetComponent<Image>();
        //subscribe to the event for switching the icons
        EventManager.changeCurrentlyEquippedIconEvent += UpdateCurrentlyHeldItemIcon;
    }

    //method for udpating the currently held item
    private void UpdateCurrentlyHeldItemIcon(int itemID)
    {
        currentlyHeldIcon.sprite = heldIcons[itemID];
    }

    private void OnDestroy()
    {
        //unscrubscribe
        EventManager.changeCurrentlyEquippedIconEvent -= UpdateCurrentlyHeldItemIcon;
    }
}
