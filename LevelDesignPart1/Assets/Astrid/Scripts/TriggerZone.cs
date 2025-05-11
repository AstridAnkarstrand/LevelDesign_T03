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
        // To stop the Update() from being called until object's been triggered.
        enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if it's the player colliding
        if (other.GetComponent<CharacterController>() == null) return;

        // Disable the collider to avoid being triggered multiple times.
        gameObject.GetComponent<Collider>().enabled = false;

        // Do stuff
        onFirstEnter.Invoke();

        // Start timer
        enabled = true;
    }

    void Update()
    {
        currTime += Time.deltaTime;

        if (currTime > timer)
        {
            onEnd.Invoke();
            Destroy(gameObject);
        }
    }
}
