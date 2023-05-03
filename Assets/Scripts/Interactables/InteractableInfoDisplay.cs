using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Script will control toggling off and on the spatial UI element displaying information about the interactable it is attached to
 * When the game initializes it will find an accompanying IInteraction script attached to the parent.
 * When it hears the 'toggle interactable info' event it will check if the parameter being passed is its parent; if it is it will toggle on
 * Likewise for when it toggles off.
 */
public class InteractableInfoDisplay : MonoBehaviour
{
    private IInteraction interactableObject;

    [SerializeField] private GameObject infoDisplay;

    private void OnEnable()
    {
        interactableObject = GetComponentInParent<IInteraction>();
    }

    private void Awake()
    {
        EventManager.toggleInteractableInfoDisplayEvent += ToggleActive;
    }

    private void Start()
    {
        infoDisplay.SetActive(false);
    }

    private void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);

        //Quaternion.Look(transform.rotation, Camera.main.transform.rotation, Time.deltaTime * 5);
    }

    private void ToggleActive(IInteraction _interactable, bool offOn)
    {
        if (_interactable == interactableObject)
        {
            infoDisplay.SetActive(offOn);
        }
    }

    private void OnDestroy()
    {
        EventManager.toggleInteractableInfoDisplayEvent -= ToggleActive;
    }

}
