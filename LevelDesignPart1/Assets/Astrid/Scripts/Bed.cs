using UnityEngine;
using UnityEngine.Events;

public class Bed : MonoBehaviour, IInteract
{
    [SerializeField]
    UnityEvent onInteract = default;

    public void Interact(Transform transform)
    {
        onInteract.Invoke();
        Destroy(this);
    }

    public bool GetIsInteractable()
    {
        return true;
    }
}
