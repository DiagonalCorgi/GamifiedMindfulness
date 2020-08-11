using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSource : MonoBehaviour
{
    public EventInstance trainMetronome;
    public EventInstance rhythm;

    [Range(0.0F, 2.0F)]
    public float audioSpeed = 1.5f;

    public int rhythmIndex = 1;

    void Start()
    {
        trainMetronome = SoundM.CreateSoundInstance("TrainMetronome", transform, GetComponent<Rigidbody>());
        rhythm = SoundM.CreateSoundInstance("Forest", transform, GetComponent<Rigidbody>());

        SoundM.PlaySound(trainMetronome);
        SoundM.PlaySound(rhythm);
        SoundM.SetSoundParameter("TrackVolume", 1);
    }

    void Update()
    {
        SoundM.SetSoundParameter("Speed", audioSpeed);
        SoundM.SetSoundParameter("RhythmIndex", (float)rhythmIndex);
    }
}
