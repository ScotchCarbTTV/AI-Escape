using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Navpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventManager.updateNavPositionEvent += UpdatePosition;
    }


    // Update is called once per frame
    void Update()
    {

    }

    private void UpdatePosition(Vector3 newPos)
    {
        transform.position = newPos + new Vector3(0, 1, 0);
        //Debug.Log("Updated the position of the navigation point for the player");
    }

    private void OnDestroy()
    {
        EventManager.updateNavPositionEvent -= UpdatePosition;
    }
}
