using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() == null) return;

        // TODO: Add to score type thing

        Destroy(gameObject);
    }
}
