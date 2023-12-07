using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool IsOpen = false;
    public bool IsAutomaticClose = false;
    [SerializeField]
    private bool IsRotatingDoor = true;
    [Tooltip("Door opens at Play.")]
    [SerializeField] bool IsStartOpen = false;
    [Tooltip("Multiplier, higher value means the door closes quicker.")]
    [SerializeField]
    private float OpenSpeed = 1f;
    [Tooltip("Multiplier, higher value means the door closes quicker.")]
    [SerializeField]
    private float CloseSpeed = 1f;
    [Tooltip("Multiplier, higher value means the door closes quicker.")]
    [SerializeField]
    private float AutomaticCloseSpeed = 2f;
    [Header("Rotation Configs")]
    [SerializeField]
    private float RotationAmount = 90f;
    [SerializeField]
    private float ForwardDirection = 0;
    [Header("Sliding Configs")]
    [SerializeField]
    private Vector3 SlideDirection = Vector3.back;
    [SerializeField]
    private float SlideAmount = 1.9f;
    [Header("Audio")]
    [SerializeField] AudioClip OpenDoorSFX, CloseDoorSFX, SlamDoorSFX;

    private Vector3 StartRotation;
    private Vector3 StartPosition;
    private Vector3 Forward;

    private Coroutine AnimationCoroutine;

    AudioSource _AudioSource;

    public bool GetIsRotatingDoor() { return IsRotatingDoor; }

    private void Awake()
    {
        StartRotation = transform.rotation.eulerAngles;
        // Since "Forward" actually is pointing into the door frame, choose a direction to think about as "forward" 
        Forward = transform.forward;
        StartPosition = transform.position;

        if (IsStartOpen)
        {
            IsOpen = true;
            transform.rotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y - RotationAmount, StartRotation.z));
        }

        _AudioSource = transform.AddComponent<AudioSource>();
    }

    public void Open(Vector3 UserPosition)
    {
        if (!IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                float dot = Vector3.Dot(Forward, (UserPosition - transform.position).normalized);
                AnimationCoroutine = StartCoroutine(DoRotationOpen(dot));
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingOpen());
            }
        }
    }

    private IEnumerator DoRotationOpen(float ForwardAmount)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation;

        if (ForwardAmount >= ForwardDirection)
        {
            endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y + RotationAmount, StartRotation.z));
        }
        else
        {
            endRotation = Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y - RotationAmount, StartRotation.z));
        }

        IsOpen = true;

        // Audio
        PlaySound(OpenDoorSFX);

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            time += Time.deltaTime * OpenSpeed;
        }

        transform.rotation = endRotation;
    }

    private IEnumerator DoSlidingOpen()
    {
        Vector3 endPosition = StartPosition + SlideAmount * SlideDirection;
        Vector3 startPosition = transform.position;

        float time = 0;
        IsOpen = true;

        // Audio
        PlaySound(OpenDoorSFX);

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * OpenSpeed;
        }

        transform.position = endPosition;
    }

    public void Close(bool IsAutoClose)
    {
        if (IsOpen)
        {
            if (AnimationCoroutine != null)
            {
                StopCoroutine(AnimationCoroutine);
            }

            if (IsRotatingDoor)
            {
                AnimationCoroutine = StartCoroutine(DoRotationClose(IsAutoClose));
            }
            else
            {
                AnimationCoroutine = StartCoroutine(DoSlidingClose());
            }
        }
    }

    private IEnumerator DoRotationClose(bool IsAutoClose)
    {
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(StartRotation);

        IsOpen = false;

        // Audio
        PlaySound(IsAutoClose ? SlamDoorSFX : CloseDoorSFX);

        float time = 0;
        while (time < 1)
        {
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, time);
            yield return null;
            float speed = IsAutoClose ? AutomaticCloseSpeed : CloseSpeed;
            time += Time.deltaTime * speed;
        }

        transform.rotation = endRotation;
    }

    private IEnumerator DoSlidingClose()
    {
        Vector3 endPosition = StartPosition;
        Vector3 startPosition = transform.position;
        float time = 0;

        IsOpen = false;

        // Audio
        PlaySound(CloseDoorSFX);

        while (time < 1)
        {
            transform.position = Vector3.Lerp(startPosition, endPosition, time);
            yield return null;
            time += Time.deltaTime * CloseSpeed;
        }

        transform.position = endPosition;
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            _AudioSource.PlayOneShot(clip, 0.2f);
        }
    }
}
