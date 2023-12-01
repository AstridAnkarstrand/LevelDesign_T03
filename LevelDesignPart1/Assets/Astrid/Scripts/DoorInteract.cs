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
        if (other.GetComponent<CharacterController>()  != null )
        {
            OpenDoor();
        }
        // TODO: Check if they press the Interact action
        // Open door
    }

    // For testing, should be removed latter!
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        // TODO: Check if they press the Interact action
        // Close door
        if (other.GetComponent<CharacterController>() != null)
        {
            CloseDoor();
        } 
    }

    void OpenDoor()
    {
        float yRotation = rotateIn ? -openRotationMax : openRotationMax;

        //door.rotation = new Quaternion(0, yRotation, 0, 0);

        door.Rotate(0, yRotation, 0.0f, Space.Self);
        isOpened = true;
    }

    void CloseDoor() 
    {
        door.rotation = Quaternion.identity;
        isOpened = false;
    }
}
