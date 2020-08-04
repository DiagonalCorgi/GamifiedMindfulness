using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundM
{
    public static FMOD.Studio.EventInstance CreateSoundInstance(string name)
    {
        EventInstance sound = FMODUnity.RuntimeManager.CreateInstance("event:/" + name);

        return sound;
    }

    public static FMOD.Studio.EventInstance CreateSoundInstance(string name, Vector3 position)
    {
        EventInstance sound = FMODUnity.RuntimeManager.CreateInstance("event:/" + name);
        sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));

        return sound;
    }

    public static FMOD.Studio.EventInstance CreateSoundInstance(string name, Transform transform, Rigidbody rb)
    {
        EventInstance sound = FMODUnity.RuntimeManager.CreateInstance("event:/" + name);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, rb);

        return sound;
    }

    public static void PlaySound(FMOD.Studio.EventInstance sound)
    {
        sound.start();
    }

    public static void PlaySound(FMOD.Studio.EventInstance sound, Vector3 position)
    {
        sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
        sound.start();
    }

    public static void PlaySound(FMOD.Studio.EventInstance sound, GameObject gObject)
    {
        sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gObject));
        sound.start();
    }

    public static void PlaySound(FMOD.Studio.EventInstance sound, Transform transform, Rigidbody rb)
    {
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound, transform, rb);
        sound.start();
    }

    public static void UpdateSoundPos(FMOD.Studio.EventInstance sound, Vector3 position)
    {
        sound.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(position));
    }

    public static void SetSoundParameter(FMOD.Studio.EventInstance sound, string parameterName, float value)
    {
        sound.setParameterByName(parameterName, value);
    }

    public static void SetSoundParameter(string parameterName, float value)
    {
        FMODUnity.RuntimeManager.StudioSystem.setParameterByName(parameterName, value);
    }
}
