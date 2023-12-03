using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, IInteract
{
    public void Interact(Transform transform)
    {
        Debug.Log("Collectable taken!");

        //
        // TODO: Add to score type thing

        Destroy(gameObject);
    }

    public bool GetIsInteractable()
    {
        return true;
    }
}
