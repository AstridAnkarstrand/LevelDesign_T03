using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Car : MonoBehaviour, IInteract
{
    [SerializeField]
    UnityEvent onInteract = default;

    public void Interact(Transform transform)
    {
        onInteract.Invoke();
    }

    public bool GetIsInteractable()
    {
        return true;
    }
}
