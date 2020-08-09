using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ControllerVibration : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction; //Vibration action
    public SteamVR_Action_Boolean trackpadAction; //note this is for vive Trigger to haptic when button when button pressed (testing)

    // Start is called before the first frame update
    // Use this later to call constant haptic
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Left hand
        if (trackpadAction.GetStateDown(SteamVR_Input_Sources.LeftHand))
        {
            Haptics(1, 150, 75, SteamVR_Input_Sources.LeftHand);
        }

        // Right Hand
        if (trackpadAction.GetStateDown(SteamVR_Input_Sources.RightHand))
        {
            Haptics(1, 150, 75, SteamVR_Input_Sources.RightHand);
        }
    }

    private void Haptics(float duration, float frequency, float amplitude, SteamVR_Input_Sources source)
    {
        hapticAction.Execute(0, duration, frequency, amplitude, source);

        // debug
        print("Haptic " + source.ToString());
    }
}
