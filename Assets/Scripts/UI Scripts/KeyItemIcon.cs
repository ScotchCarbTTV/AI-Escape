using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 *  Script is to be attached to a KeyItemIcon object.
 *  Responsible for handling information about a key item status eg obtained/not obtained
 *  
 */

public class KeyItemIcon : MonoBehaviour
{
    private bool obtained;
    public bool Obtained { get { return obtained; } }

    [SerializeField] string keyItemID;
    public string KeyItemID { get { return keyItemID; } }

    private Image iconDisplay;

    [SerializeField] Sprite notObtainedDisplay;

    [SerializeField] Sprite obtainedDisplay;

    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent<Image>(out iconDisplay))
        {
            Debug.LogError("You forgot to attach this to something with an Image component");
        }

        //subscribe to UpdateInventoryIcon even
        EventManager.toggleKeyItemIconEvent += UpdateInventoryIcon;
    }

    // Update is called once per frame
    void Update()
    {
        
    } 

    private void UpdateInventoryIcon(bool onOff, string _keyItemID)
    {
        if (_keyItemID == keyItemID)
        {
            if (onOff)
            {
                iconDisplay.sprite = obtainedDisplay;
            }            
        }
    }

    private void OnDestroy()
    {
        EventManager.toggleKeyItemIconEvent -= UpdateInventoryIcon;
    }

}
