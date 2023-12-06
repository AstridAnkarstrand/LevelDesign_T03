using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressCloseShopTracker : MonoBehaviour
{
    [SerializeField] Switch[] progressArray;
    [SerializeField] DoorTrigger unlockObject;

    int progress = 0;

    private void Update()
    {
        progress = 0;
        foreach (Switch _switch in progressArray)
        {
            if (_switch.GetIsFlipped())
            {
                progress++;
            }
        }

        if (progress == progressArray.Length)
        {
            Unlock();
        }
    }

    private void Unlock()
    {
        Debug.Log("Objective completed!");
        // Unlock the objective
        unlockObject.SetIsInputBased(true);
        gameObject.SetActive(false);
    }

}
