using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSource : MonoBehaviour
{
    public EventInstance trainMetronome;

    [Range(0.0F, 2.0F)]
    public float audioSpeed = 1.5f;

    void Start()
    {
        trainMetronome = SoundM.CreateSoundInstance("TrainMetronome", transform, GetComponent<Rigidbody>());

        SoundM.PlaySound(trainMetronome);
    }

    void Update()
    {
        SoundM.SetSoundParameter("Speed", audioSpeed);
    }
}
