using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerVibration : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction; //Vibration action
    public SteamVR_Action_Boolean trackpadAction; //note this is for vive Trigger to haptic when button when button pressed (testing)

    void Start()
    {
        InvokeRepeating("LeftHaptics", 2.0f, 2.0f);
        //InvokeRepeating("RightHaptics", 2.0f, 2.0f);
        Invoke("RepeatRightHaptics", 1.0f);

    }

    // Update is called once per frame
    
    
    void Update()
    {
        
    }
 
    //Haptics function for Left Hand
    private void LeftHaptics()
    {
        Haptics(5, 170, 75, SteamVR_Input_Sources.LeftHand);
    }


    // Haptics function for Right Hand
    private void RightHaptics()
    {
        Haptics(5, 170, 75, SteamVR_Input_Sources.RightHand);
    }

    // To run in repeatedly after n seconds
    private void RepeatRightHaptics()
    {
        InvokeRepeating("RightHaptics", 2.0f, 2.0f);
    }

    // The part that actually triggers the haptic to happen
    private void Haptics(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);

        // debug
        print("Haptic " + source.ToString());
    }
}
