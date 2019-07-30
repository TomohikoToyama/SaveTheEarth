using System.Collections;
using UnityEngine;

public class LeftController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Hand")
        SoundManager.Instance.PlaySE(0);

       // OVRHaptics.LeftChannel;
    }
}
