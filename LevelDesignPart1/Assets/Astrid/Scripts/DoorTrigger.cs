using UnityEngine;
using System.Collections;

public class DoorTrigger : MonoBehaviour
{
    [SerializeField] Door Door;
    [SerializeField] float WaitBeforeCloseTime = 0f;

    Coroutine CloseTimerCoroutine;
    Transform Player;

    private void Update()
    {
        if (Player == null) return;

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
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CharacterController controller))
        {
            Player = other.transform;
            /*
            if (CloseTimerCoroutine != null)
            {
                StopCoroutine(CloseTimerCoroutine);
            }

            if (!Door.IsOpen)
            {
                Door.Open(other.transform.position);
            }*/
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
