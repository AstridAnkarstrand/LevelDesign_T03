using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    [SerializeField] Transform door;
    [SerializeField] float openRotationMax;
    [SerializeField] bool rotateIn;

    bool isOpened;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        // TODO: Check if it's the player
        // TODO: Check if they press the Interact action
        // Open door
        OpenDoor();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        // TODO: Check if it's the player
        // TODO: Check if they press the Interact action
        // Close door
        CloseDoor();
    }

    void OpenDoor()
    {
        float yRotation = rotateIn ? -openRotationMax : openRotationMax;

        door.rotation = new Quaternion(0, yRotation, 0, 0);
        isOpened = true;
    }

    void CloseDoor() 
    {
        door.rotation = Quaternion.identity;
        isOpened = false;
    }
}
