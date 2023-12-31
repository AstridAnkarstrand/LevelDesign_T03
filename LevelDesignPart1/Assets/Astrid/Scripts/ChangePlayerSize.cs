using UnityEngine;

public class ChangePlayerSize : MonoBehaviour
{
    [SerializeField] GameObject Head;
    [SerializeField] Camera _Camera;
    [SerializeField] float HeadYPos, FOV, Heigth;
    [SerializeField] AudioClip TransitionSFX;

    AudioSource _AudioSource;

    float StartHeadYPos, StartFOV, StartHeight;

    bool IsChild = false;

    CharacterController _CharacterController;

    private void Awake()
    {
        StartHeadYPos = Head.transform.localPosition.y;
        StartFOV = _Camera.fieldOfView;
        _AudioSource = GetComponent<AudioSource>();
        _CharacterController = GetComponent<CharacterController>();

        StartHeight = _CharacterController.height;
    }

    public void SwitchSize()
    {
        PlaySound();

        IsChild = !IsChild;

        Vector3 headPos = new Vector3(0, (IsChild ? HeadYPos: StartHeadYPos), 0);
        Head.transform.localPosition = headPos;
        _Camera.fieldOfView = IsChild ? FOV : StartFOV;

        Vector3 center = new Vector3(0, (IsChild ? Heigth : StartHeight)/ 2 , 0);
        _CharacterController.center = center;
        _CharacterController.height = IsChild ? Heigth : StartHeight;
    }

    void PlaySound()
    {
        if (TransitionSFX != null)
        {
            _AudioSource.PlayOneShot(TransitionSFX);
        }
    }
}
