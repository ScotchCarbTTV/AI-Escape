using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    //variable for controlling the camera height
    [SerializeField] private float cameraHeight;

    //float for conttrolling camera rotation
    private Vector3 cameraRotation;

    //reference to the player object
    [SerializeField] private GameObject player;

    private Vector3 cameraOffsetPos;

    //enum for the camera facing direction
    private enum CameraDirection { north, east, south, west }

    [SerializeField] private CameraDirection cameraDirection;

    void Start()
    {
        cameraOffsetPos = player.transform.position + new Vector3(0, cameraHeight, 0);

        cameraRotation = new Vector3(0, 0, 0);

        //subscribe to events?
    }


    void Update()
    {
        //detect player input to rotate the camera 90 degree left or right
        if (Input.GetKeyDown(KeyCode.Q))
        {
            //call rotate camera method to turn left 90 degrees
            Rotatecamera(cameraDirection, 0);
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            //call wrasdfdsd
            Rotatecamera(cameraDirection, 1);
        }

        UpdateCameraPosition();
    }

    private void UpdateCameraPosition()
    {
        cameraOffsetPos = player.transform.position + new Vector3(0, cameraHeight, 0);

        if (transform.rotation.eulerAngles != cameraRotation)
        {
            transform.rotation = Quaternion.Euler(Vector3.Slerp(transform.rotation.eulerAngles, cameraRotation, Time.deltaTime * 5));
        }

        //move the camera container to be 10 units above the player position.
        if (Vector3.Distance(transform.position, cameraOffsetPos) > 10 && Vector3.Distance(transform.position, cameraOffsetPos) < 15)
        {
            transform.position = Vector3.Slerp(transform.position, cameraOffsetPos, Time.deltaTime * 5);
        }
        else
        {
            transform.position = Vector3.Slerp(transform.position, cameraOffsetPos, Time.deltaTime * 15);
        }
    }

    private void Rotatecamera(CameraDirection currDir, int turnDir)
    {
        switch (currDir)
        {
            case CameraDirection.north:
                //facing north already? Go to east facing.

                if (turnDir == 1)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);

                    cameraRotation = new Vector3(0, 90, 0);
                    cameraDirection = CameraDirection.east;
                }
                else
                {
                    //transform.rotation = Quaternion.Euler(0, 270, 0);
                    transform.rotation = Quaternion.Euler(0, 359, 0);
                    cameraRotation = new Vector3(0, 270, 0);
                    cameraDirection = CameraDirection.west;
                }
                break;
            case CameraDirection.east:
                //facing east? go to south facing.                
                if (turnDir == 1)
                {
                    //transform.rotation = Quaternion.Euler(0, 180, 0);
                    cameraRotation = new Vector3(0, 180, 0);
                    cameraDirection = CameraDirection.south;
                }
                else
                {
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
                    cameraRotation = new Vector3(0, 0, 0);
                    cameraDirection = CameraDirection.north;
                }
                break;
            case CameraDirection.south:
                //facing south? 

                if (turnDir == 1)
                {
                    //transform.rotation = Quaternion.Euler(0, 270, 0);
                    cameraRotation = new Vector3(0, 270, 0);
                    cameraDirection = CameraDirection.west;
                }
                else
                {
                    //transform.rotation = Quaternion.Euler(0, 90, 0);
                    cameraRotation = new Vector3(0, 90, 0);
                    cameraDirection = CameraDirection.east;
                }
                break;
            case CameraDirection.west:
                //facing west? turn north

                if (turnDir == 1)
                {
                    //transform.rotation = Quaternion.Euler(0, 0, 0);
                    cameraRotation = new Vector3(0, 359, 0);
                    cameraDirection = CameraDirection.north;
                }
                else
                {
                    //transform.rotation = Quaternion.Euler(0, 180, 0);
                    cameraRotation = new Vector3(0, 180, 0);
                    cameraDirection = CameraDirection.south;
                }

                break;
        }
    }
}
