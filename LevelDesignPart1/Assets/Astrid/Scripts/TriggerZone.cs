using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    [SerializeField]
    UnityEvent onFirstEnter = default, onEnd = default;
    [SerializeField] float timer;

    float currTime;

    void Awake()
    {
        enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterController>() == null) return;

        // Do stuff
        onFirstEnter.Invoke();
        enabled = true;
    }

    void Update()
    {
        if (!enabled) return;

        currTime += Time.deltaTime;

        if (currTime > timer)
        {
            onEnd.Invoke();
            enabled = false;
            Destroy(gameObject);
        }
    }
}
