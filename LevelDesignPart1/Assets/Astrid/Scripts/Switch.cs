using UnityEngine;

public class Switch : MonoBehaviour, IInteract
{
    [SerializeField] GameObject[] Objects;
    [SerializeField] Transform Button;

    Vector3 StartRotation;
    bool IsFlipped = false;

    public bool GetIsFlipped() { return IsFlipped; }

    private void Awake()
    {
        StartRotation =  Button.rotation.eulerAngles;
    }


    public bool GetIsInteractable()
    {
        return true;
    }

    public void Interact(Transform transform)
    {
        IsFlipped = !IsFlipped;
        Quaternion flipRotation = IsFlipped ? Quaternion.Euler(new Vector3(-StartRotation.x, StartRotation.y, StartRotation.z)) 
            : Quaternion.Euler(new Vector3(StartRotation.x, StartRotation.y, StartRotation.z));
        Button.rotation = flipRotation;

        foreach(var obj in Objects)
        {
            obj.SetActive(!obj.activeInHierarchy);
        }
    }
}
