using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//parent class for all key items

public class IKeyItem : MonoBehaviour, IInteraction
{
    //the ID of the item that is being collected
    public enum KeyID { GreenKey, YellowKey, RedKey};
    public KeyID keyID;

    private List<string> keyIDStrings = new List<string>() {"GreenKey", "YellowKey", "RedKey" };

    private bool playerProx = false;

    private void OnEnable()
    {
    }

    public void Activate()
    {
        if (CheckAvail() == true)
        {
            EventManager.gainKeyItemEvent(keyIDStrings[(int)keyID]);
            gameObject.SetActive(false);
        }
        else
        {
            
        }        
    }

    public void ToggleInteract(bool toggle)
    {
        if (toggle == true)
        {
            Debug.Log("Player in range");
        }
        else
        {
            Debug.Log("Player leaving byeee");
        }
        playerProx = toggle;
    }

    public bool CheckAvail()
    {
        return playerProx;
    }
}
