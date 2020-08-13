using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerManager : MonoBehaviour
{
    public AudioSource audioManager;

    public EventInstance poof;

    public GameObject[] handObject;

    void Start()
    {
        poof = SoundM.CreateSoundInstance("Poof");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 handVel;
        Vector3 handAngVel;

        for (int hand = 0; hand < handObject.Length; hand++)
        {
            handObject[hand].GetComponent<Hand>().GetEstimatedPeakVelocities(out handVel, out handAngVel);
            if (handVel.magnitude > 1f)
            {
                handObject[hand].GetComponent<Collider>().enabled = true;
            }
            else
            {
                handObject[hand].GetComponent<Collider>().enabled = false;
            }
        }
    }

    public void Hit(int hand)
    {
        if (audioManager.checkBeatHit(hand))
        {
            SoundM.PlaySound(poof, handObject[hand].transform.position);
        }
    }
}
