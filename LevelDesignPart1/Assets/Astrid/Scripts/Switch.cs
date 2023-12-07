using Unity.VisualScripting;
using UnityEngine;

public class Switch : MonoBehaviour, IInteract
{
    [SerializeField] GameObject[] Objects;
    [SerializeField] Transform Button;
    [SerializeField] AudioClip[] SwitchSFXs;

    AudioSource _AudioSource;
    Vector3 StartRotation;
    bool IsFlipped = false;

    public bool GetIsFlipped() { return IsFlipped; }

    private void Awake()
    {
        StartRotation =  Button.rotation.eulerAngles;
        _AudioSource = transform.AddComponent<AudioSource>();
    }


    public bool GetIsInteractable()
    {
        return true;
    }

    public void Interact(Transform transform)
    {
        PlaySFX();
        IsFlipped = !IsFlipped;
        Quaternion flipRotation = IsFlipped ? Quaternion.Euler(new Vector3(-StartRotation.x, StartRotation.y, StartRotation.z)) 
            : Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y, StartRotation.z));
        Button.rotation = flipRotation;

        foreach(var obj in Objects)
        {
            obj.SetActive(!obj.activeInHierarchy);
        }
    }

    void PlaySFX()
    {
        if (SwitchSFXs.Length > 0)
        {
            int i = Random.Range(0, SwitchSFXs.Length - 1);
            _AudioSource.PlayOneShot(SwitchSFXs[i]);
        }
    }
}
