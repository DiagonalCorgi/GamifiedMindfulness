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

    //Rhthym Acuracy Tracking
    public int missAllowance;
    public int consecutiveMisses;

    public int streakCounter;
    public float focus;

    [Range(0F, 60F)]
    public float streakLength = 10;
    public float streakTimer = -1;

    public GameObject fallbackLeftHand;
    public GameObject fallbackRightHand;

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

        if (Input.GetButtonDown("Fire1"))
        {
            fallbackLeftHand.SetActive(true);
            //Hit(0);

            Invoke("DisableFallbackHands", 0.2f);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            fallbackRightHand.SetActive(true);
            //Hit(1);

            Invoke("DisableFallbackHands", 0.2f);
        }
    }

    public void Hit(int hand)
    {
        if (audioManager.checkBeatHit(hand))
        {
            SoundM.PlaySound(poof, handObject[hand].transform.position);
            //streakCounter++;
            //Debug.Log("hit streak: " + streakCounter);
        }
        /*else if (!audioManager.checkBeatHit(hand))
        {
            streakCounter == 0;
        }*/
    }

    public void DisableFallbackHands()
    {
        fallbackLeftHand.SetActive(false);
        fallbackRightHand.SetActive(false);
    }
}
