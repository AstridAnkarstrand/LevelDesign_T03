using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Code: https://www.youtube.com/watch?v=cPltQK5LlGE&t=311s&ab_channel=LlamAcademy 
public class DoorInteract : MonoBehaviour
{
    [SerializeField] Transform door;
    [SerializeField] float openRotationMax = 90f;
    [SerializeField] bool closeAuto; // Should the door close on it's own?
    [SerializeField] float Speed = 1f;
    [SerializeField] LayerMask layer;
    [SerializeField] bool Rotated90;

    Vector3 startRotation;
    Vector3 Forward;
    bool IsOpen;

    Coroutine AnimationCoroutine;

    private void Start()
    {
        startRotation = door.rotation.eulerAngles;
        if (Rotated90)
            Forward = door.right;
        else
            Forward = door.forward;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger Entered");
        if (IsOpen) return;
        if (other.GetComponent<CharacterController>()  != null )
        {
            OpenDoor(other.transform.position);
        }
    }

    // For testing, should be removed latter!
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Trigger Exit");
        if (other.GetComponent<CharacterController>() != null)
        {
            if (closeAuto)
                CloseDoor();
        } 
    }

    void OpenDoor(Vector3 playerPosition)
    {
        if (IsOpen) return;

        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        // Check if player is in front of or behind door so it can rotate away from the player.
        float dot = Vector3.Dot(Forward, (playerPosition - door.position).normalized);

        AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
    }

    IEnumerator DoRotationOpen(float forwardAmount)
    {
        Quaternion start = door.rotation;
        Quaternion end;

        if (forwardAmount >= 0)
        {
            end = Quaternion.Euler(new Vector3(0, start.y + openRotationMax, 0));
        } else
        {
            end = Quaternion.Euler(new Vector3(0, start.y - openRotationMax, 0));
        }

        IsOpen = true;

        float time = 0;
        while (time < 1)
        {
            door.rotation = Quaternion.Slerp(start, end, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        door.rotation = end;
    }

    void CloseDoor()
    {
        if (!IsOpen) return;

        if (AnimationCoroutine != null)
        {
            StopCoroutine(AnimationCoroutine);
        }

        AnimationCoroutine = StartCoroutine(DoRotationClose());
    }

    IEnumerator DoRotationClose()
    {
        Quaternion start = door.rotation;
        Quaternion end = Quaternion.Euler(startRotation);

        IsOpen = false;

        float time = 0;
        while (time < 1)
        {
            door.rotation = Quaternion.Slerp(start, end, time);
            yield return null;
            time += Time.deltaTime * Speed;
        }

        door.rotation = end;
    }
}
