using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] GameObject InteractUI;
    [SerializeField] float InteractMaxDistance = 4f;
    [SerializeField] LayerMask InteractLayer;


    Transform Camera;

    private void Awake()
    {
        InteractUI.SetActive(false);
        Camera = transform.GetComponentInChildren<Camera>().transform;
    }

    private void Update()
    {
        // Set interact UI;
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, InteractMaxDistance, InteractLayer)
           && hit.collider.TryGetComponent(out IInteract iInteract))
        {
            InteractUI.SetActive(iInteract.GetIsInteractable());
        }
        else
        {
            InteractUI.SetActive(false);
        }

        // Player Input
        if (Input.GetMouseButtonDown(0))
        {
            Interact();
        }
    }

    void Interact()
    {
        if (Physics.Raycast(Camera.position, Camera.forward, out RaycastHit hit, InteractMaxDistance, InteractLayer) 
            && hit.collider.TryGetComponent(out IInteract iInteract))
        {
            if (iInteract.GetIsInteractable())
            {
                iInteract.Interact(transform);
            }
        }
    }
}
