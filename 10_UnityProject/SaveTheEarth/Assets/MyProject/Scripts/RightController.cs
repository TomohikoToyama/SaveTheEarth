using System.Collections;
using UnityEngine;

public class RightController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag != "Hand")
        SoundManager.Instance.PlaySE(0);
    }
}
