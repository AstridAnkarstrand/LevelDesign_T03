using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Door Door;
    [SerializeField] float WaitBeforeCloseTime = 0f;
    [Tooltip("Should the countdown for automatic close be on door opened or on trigger exited." +
        "Does nothing if the Door isn't autoclose.")]
    [SerializeField] bool IsCloseOnTriggerExit = true;
    [Tooltip("Does the door require input from player to open and close?")]
    [SerializeField] bool IsInputBased = true;

    Coroutine CloseTimerCoroutine;
    Transform Player;

    private void Update()
    {
        if (Player == null) return;
        //if (!Door.GetIsRotatingDoor()) return;
        if (!IsInputBased) return;

        // Input
        if (Input.GetMouseButtonDown(0))
        {
            if (Door.IsOpen)
            {
                // Close door
                Door.Close(false);
            } else
            {
                // Open door
                if (CloseTimerCoroutine != null)
                {
                    StopCoroutine(CloseTimerCoroutine);
                }

                Door.Open(Player.position);

                if (!IsCloseOnTriggerExit & Door.IsAutomaticClose)
                    CloseTimerCoroutine = StartCoroutine(WaitBeforeAutomaticClose());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            Player = other.transform;

            if (!IsInputBased)
            {
                // Open door
                if (CloseTimerCoroutine != null)
                {
                    StopCoroutine(CloseTimerCoroutine);
                }

                Door.Open(Player.position);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            Player = null;
            if (Door.IsOpen)
            {
                if (Door.IsAutomaticClose)
                {
                    CloseTimerCoroutine = StartCoroutine(WaitBeforeAutomaticClose());
                }
            }
        }
    }

    IEnumerator WaitBeforeAutomaticClose()
    {
        yield return new WaitForSeconds(WaitBeforeCloseTime);
        Door.Close(true);
    }
}
