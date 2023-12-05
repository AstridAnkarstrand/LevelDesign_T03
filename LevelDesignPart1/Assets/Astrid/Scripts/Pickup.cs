using UnityEngine;
using UnityEngine.Events;

public class Pickup : MonoBehaviour, IInteract
{
    [SerializeField]
    UnityEvent onPickup = default;

    public void Interact(Transform transform)
    {
        Debug.Log("Collectable taken!");

        //
        // TODO: Add to score type thing
        onPickup.Invoke();

        Destroy(gameObject);
    }

    public bool GetIsInteractable()
    {
        return true;
    }
}
