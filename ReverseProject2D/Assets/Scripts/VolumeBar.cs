using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeBar : MonoBehaviour
{
    void Awake()
    {
        Slider bar = GetComponent<Slider>();

        bar.value = SoundManager.instance.mainVolume;
    }
}