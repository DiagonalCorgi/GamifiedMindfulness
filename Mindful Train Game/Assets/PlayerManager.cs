using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PlayerManager : MonoBehaviour
{
    public AudioSource audioManager;

    public EventInstance poof;
    public EventInstance transition;

    public GameObject[] handObject;

    //Rhthym Acuracy Tracking
    public int missAllowance;
    public int consecutiveMisses;

    public int streakCounter;
    public float focus;

    [Range(0F, 1F)]
    public float steamAlpha = 1;

    [Range(0F, 60F)]
    public float streakLength = 10;
    public float streakTimer = -1;

    [Range(0F, 60F)]
    public float transitionTimerLength = 6;
    public float transitionTimer = 0;

    public GameObject fallbackLeftHand;
    public GameObject fallbackRightHand;

    public CloudCall trainSteamController;

    void Start()
    {
        poof = SoundM.CreateSoundInstance("Poof");
        transition = SoundM.CreateSoundInstance("Transition");

        InvokeRepeating("decreaseMissCounter", 2, 6);
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

        if (consecutiveMisses > missAllowance)
        {
            //reset streak timer and counters
            consecutiveMisses = 0;
            streakCounter = 0;

            streakTimer = -1;
        }

        if (streakTimer > 0)
        {
            streakTimer -= Time.deltaTime;
        }
        else if (streakTimer != -1)
        {
            if (audioManager.rhythmIndex < audioManager.rightHandRhythms.Count - 1)
            {
                audioManager.rhythmIndex++;
                streakTimer = -1;
                streakCounter = 0;
                transitionTimer = transitionTimerLength;

                SoundM.PlaySound(transition, transform.position);
            }
        }

        SoundM.UpdateSoundPos(transition, transform.position);

        //increase focus based on streak timer
        if (streakTimer >= 0 && consecutiveMisses <= 2)
        {
            focus = Mathf.Clamp(1 / streakLength * (streakLength - streakTimer) * 1.5f, 0, 1);
        }
        else
        {
            focus = 0;
        }

        if (transitionTimer > 0)
        {
            transitionTimer -= Time.deltaTime;
        }
        else
        {
            transitionTimer = 0;
        }

        if (transitionTimer > 0)
        {
            //during transition make steam invisible
            steamAlpha += (0 - steamAlpha) * 0.01f;
        }
        else
        {
            //adjust steam alpha based of level of focus
            steamAlpha += (Mathf.Clamp(0.2f - (Mathf.Max(focus - 0.6f, 0) * 2), 0f, 1) - steamAlpha) * 0.01f;
        }
        trainSteamController.steamAlpha = steamAlpha;

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
            if (streakCounter == 0)
            {
                streakTimer = streakLength;
            }

            SoundM.PlaySound(poof, handObject[hand].transform.position);
            streakCounter++;
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

    private void decreaseMissCounter()
    {
        if (consecutiveMisses > 0)
        {
            consecutiveMisses--;
        }
    }
}
