using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityDetector : MonoBehaviour
{
    //[SerializeField] MeshRenderer renderer;
    [SerializeField] IInteraction interaction;

    private void Start()
    {
        //renderer = GetComponentInParent<MeshRenderer>();
        interaction = GetComponentInParent<IInteraction>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerInteraction _interaction))
        {
            //GetComponent<Renderer>().material.color = Color.red;
            
           

            interaction.ToggleInteract(true);
            //Debug.Log("There's a player in my radius");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerInteraction _interaction))
        {
            

            //GetComponent<Renderer>().material.color = Color.blue;
            interaction.ToggleInteract(false);
            //Debug.Log("Goodbyepla");
        }
    }
}
