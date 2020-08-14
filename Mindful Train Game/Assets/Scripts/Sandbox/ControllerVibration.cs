using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerVibration : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction; //Vibration action
    public SteamVR_Action_Boolean trackpadAction; //note this is for vive Trigger to haptic when button when button pressed (testing)

    public AudioSource audioManager;
    private float vibrationTimeout = 0;
    [Range(-0.5F, 0.5F)]
    public float vibrationOffset = 0.15f;
    [Range(0F, 7F)]
    public float vibrationDuration = 1f;
    [Range(0F, 250F)]
    public float vibrationFrequency = 150f;
    [Range(0F, 100F)]
    public float vibrationAmplitude = 75f;

    void Start()
    {
        //InvokeRepeating("LeftHaptics", 2.0f, 2.0f);
        //InvokeRepeating("RightHaptics", 2.0f, 2.0f);
        //Invoke("RepeatRightHaptics", 1.0f);

    }

    // Update is called once per frame
    
    
    void Update()
    {

        //activating controller vibration
        vibrationTimeout -= (int)(Time.deltaTime * 1000);
        vibrationTimeout = Mathf.Max(vibrationTimeout, 0);

        float vibrationClock = Mathf.Repeat((float)audioManager.timelinePosition / 1000 + vibrationOffset, 2);

        if (vibrationTimeout == 0)
        {
            if (vibrationClock >= 1 && vibrationClock <= 1.08)
            {
                vibrationTimeout = 100;

                //play vibration
                RightHaptics();
            }

            if (vibrationClock >= 0 && vibrationClock <= 0.08)
            {
                vibrationTimeout = 100;

                //play vibration
                LeftHaptics();
            }
        }
    }
 
    //Haptics function for Left Hand
    private void LeftHaptics()
    {
        Haptics(vibrationDuration, vibrationFrequency, vibrationAmplitude, SteamVR_Input_Sources.LeftHand);
    }


    // Haptics function for Right Hand
    private void RightHaptics()
    {
        Haptics(vibrationDuration, vibrationFrequency, vibrationAmplitude, SteamVR_Input_Sources.RightHand);
    }

    // To run in repeatedly after n seconds
    /*private void RepeatRightHaptics()
    {
        InvokeRepeating("RightHaptics", 2.0f, 2.0f);
    }*/

    // The part that actually triggers the haptic to happen
    private void Haptics(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        if (hapticAction.active)
        {
            hapticAction.Execute(0, duration, frequency, amplitude, source);
        }

        // debug
        print("Haptic " + source.ToString());
    }
}
