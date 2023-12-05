using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour, IInteract
{
    [SerializeField] Door[] Door;
    [SerializeField] float WaitBeforeCloseTime = 0f;
    [Tooltip("Should the countdown for automatic close be on door opened or on trigger exited." +
        "Does nothing if the Door isn't autoclose.")]
    [SerializeField] bool IsCloseOnTriggerExit = true;
    [Tooltip("Does the door require input from player to open and close?")]
    [SerializeField] bool IsInputBased = true;
    [Tooltip("Does the door open automatically on trigger")]
    [SerializeField] bool IsOpenOnTrigger = false;

    Coroutine CloseTimerCoroutine;
    Transform Player;

    public void SetIsInputBased(bool isInputBased) { IsInputBased = isInputBased; }

    private void Update()
    {
        if (Player == null) return;
        if (!IsInputBased) return;

        // Input
        /*if (Input.GetMouseButtonDown(0))
        {
            Interact(Player);
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            Player = other.transform;

            if (!IsInputBased && IsOpenOnTrigger)
            {
                // Open door
                if (CloseTimerCoroutine != null)
                {
                    StopCoroutine(CloseTimerCoroutine);
                }

                foreach (Door door in Door)
                {
                    door.Open(Player.position);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            Player = null;
            foreach (Door door in Door)
            {
                if (door.IsOpen)
                {
                    if (door.IsAutomaticClose)
                    {
                        CloseTimerCoroutine = StartCoroutine(WaitBeforeAutomaticClose());
                    }
                }
            }
        }
    }

    IEnumerator WaitBeforeAutomaticClose()
    {
        yield return new WaitForSeconds(WaitBeforeCloseTime);
        foreach (Door door in Door)
            door.Close(true);
    }

    public void CloseDoor()
    {
        if (CloseTimerCoroutine != null)
        {
            StopCoroutine(CloseTimerCoroutine);
        }

        foreach (Door door in Door)
        {
            if (door.IsOpen)
            {
                // Close door
                door.Close(true);
            }
        }
    }

    public void Interact(Transform transform)
    {
        foreach (Door door in Door)
        {
            if (door.IsOpen)
            {
                // Close door
                door.Close(false);
            }
            else
            {
                // Open door
                if (CloseTimerCoroutine != null)
                {
                    StopCoroutine(CloseTimerCoroutine);
                }

                door.Open(transform.position);

                if (!IsCloseOnTriggerExit & door.IsAutomaticClose)
                    CloseTimerCoroutine = StartCoroutine(WaitBeforeAutomaticClose());
            }
        }
            
        
    }

    public bool GetIsInteractable()
    {
        return IsInputBased;
    }
}
