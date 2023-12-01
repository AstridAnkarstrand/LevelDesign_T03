using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class DetectionZone : MonoBehaviour
{

    [SerializeField]
    UnityEvent onFirstEnter = default, onLastExit = default;

    [SerializeField] bool onlyPlayer = false;

    List<Collider> colliders = new List<Collider>();

    void Awake()
    {
        enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (onlyPlayer && other.GetComponent<CharacterController>() == null)
        {
            return;
        }

        if (colliders.Count == 0)
        {
            onFirstEnter.Invoke();
            enabled = true;
        }
        colliders.Add(other);
    }

    void OnTriggerExit(Collider other)
    {
        if (onlyPlayer && other.GetComponent<CharacterController>() == null)
        {
            return;
        }

        if (colliders.Remove(other) && colliders.Count == 0)
        {
            onLastExit.Invoke();
            enabled = false;
        }
    }

    void FixedUpdate()
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            Collider collider = colliders[i];
            if (!collider || !collider.gameObject.activeInHierarchy)
            {
                colliders.RemoveAt(i--);
                if (colliders.Count == 0)
                {
                    onLastExit.Invoke();
                    enabled = false;
                }
            }
        }
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        if (enabled && gameObject.activeInHierarchy)
        {
            return;
        }
#endif
        if (colliders.Count > 0)
        {
            colliders.Clear();
            onLastExit.Invoke();
        }
    }
}
