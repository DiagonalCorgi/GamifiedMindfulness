using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSource : MonoBehaviour
{

    public EventInstance trainMetronome;
    public EventInstance rhythm;
    public CloudCall trainSteamController;
    public GameObject player;
    public PlayerManager playerManager;

    [Range(0.0F, 2.0F)]
    public float audioSpeed = 1.5f;


    public int rhythmIndex = 1;

    [Range(0F, 1F)]
    public float beatTolerance = 0.25f;

    //Rhythms syncing
    public int timelinePosition;
    private float realSpeed;
    private float timeDelta;
    private float fmodTimeDelta;

    private int beatTimeout;
    private float leftBeatIgnore;
    private float rightBeatIgnore;
    private float currentLeftBeat;
    private float currentRightBeat;

    [Range(0F, 6F)]
    public float noteDelay = 0;

    public List<string> leftHandRhythms;
    public List<string> rightHandRhythms;
    private int lhIndex = 0;
    private int rhIndex = 0;

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
        transform.position = player.transform.position;

        SoundM.SetSoundParameter("Speed", audioSpeed);
        SoundM.SetSoundParameter("RhythmIndex", (float)rhythmIndex);

        int newTimelinePos;
        rhythm.getTimelinePosition(out newTimelinePos);

        /*fmodTimeDelta += (float)(newTimelinePos - timelinePosition) / 1000;
        timeDelta += Time.deltaTime;
        if (fmodTimeDelta < 0)
        {
            timeDelta = 0;
            fmodTimeDelta = 0;
        }
        if (fmodTimeDelta > 2)
        {

            //Debug.Log(timeDelta + " , " + fmodTimeDelta);

            realSpeed = (realSpeed + timeDelta / fmodTimeDelta) /2;
            timeDelta = 0;
            fmodTimeDelta = 0;
            Debug.Log(realSpeed);
        }*/

        beatTimeout -= (int)(Time.deltaTime * 1000);
        beatTimeout = Mathf.Max(beatTimeout, 0);

        float newTime = Mathf.Repeat((float)newTimelinePos / 1000, 4);
        float newOffsetTime = Mathf.Repeat((float)newTimelinePos / 1000 + (noteDelay /** realSpeed*/), 4);


        if (leftHandRhythms[rhythmIndex] != "" && rightHandRhythms[rhythmIndex] != "")
        {
            //parse list from string
            string[] lhArray = leftHandRhythms[rhythmIndex].Split(',');
            string[] rhArray = rightHandRhythms[rhythmIndex].Split(',');

            float lhNextBeat = Mathf.Repeat(float.Parse(lhArray[lhIndex % lhArray.Length]), 4);
            float rhNextBeat = Mathf.Repeat(float.Parse(rhArray[rhIndex % rhArray.Length]), 4);

            //releasing beat puffs
            if (beatTimeout == 0)
            {
                if (newOffsetTime >= lhNextBeat && newOffsetTime <= lhNextBeat + 0.1)
                {
                    beatTimeout = 100;

                    //play release steam puff
                    emitLeft();
                    lhIndex++;
                }

                if (newOffsetTime >= rhNextBeat && newOffsetTime <= rhNextBeat + 0.1)
                {
                    beatTimeout = 100;

                    //play release steam puff
                    emitRight();
                    rhIndex++;
                }
            }

            /*if (Input.GetButtonDown("Fire1"))
            {
                if (checkBeatHit(0))
                {
                    //Debug.Log("Hit Left Beat");
                }
                else
                {
                    //Debug.Log("Missed Left Beat");
                }
            }*/

            //check which beats are in range to be hit by left hand
            for (int i = 0; i < lhArray.Length; i++)
            {
                float beat = Mathf.Repeat(float.Parse(lhArray[i]), 4);

                if (Mathf.Abs(newTime - beat) < beatTolerance ||
                    (newTime < beatTolerance && beat >= 4 - beatTolerance) ||
                    (newTime > 4 - beatTolerance && beat <= beatTolerance))
                {
                    if (beat != currentLeftBeat)
                    {
                        //new beat
                        currentLeftBeat = beat;
                    }
                }
                else
                {
                    //no beat
                    if (beat == currentLeftBeat)
                    {
                        //check if player has hit the current beat
                        if (leftBeatIgnore != currentLeftBeat)
                        {
                            playerManager.consecutiveMisses++;
                            //leftBeatIgnore = currentLeftBeat;
                            Debug.Log("Missed Beat, Time: " + newTime);
                        }

                        currentLeftBeat = -1;
                    }
                }
            }

            //check which beats are in range to be hit by right hand
            for (int i = 0; i < rhArray.Length; i++)
            {
                float beat = Mathf.Repeat(float.Parse(rhArray[i]), 4);

                if (Mathf.Abs(newTime - beat) < beatTolerance ||
                    (newTime < beatTolerance && beat >= 4 - beatTolerance) ||
                    (newTime > 4 - beatTolerance && beat <= beatTolerance))
                {
                    if (beat != currentRightBeat)
                    {
                        //new beat
                        currentRightBeat = beat;
                    }
                }
                else
                {
                    //no beat
                    if (beat == currentRightBeat)
                    {

                        //check if player has hit the current beat
                        if (rightBeatIgnore != currentRightBeat)
                        {
                            playerManager.consecutiveMisses++;
                            //rightBeatIgnore = currentRightBeat;
                            Debug.Log("Missed Beat, Time: " + newTime);
                        }

                        currentRightBeat = -1;
                    }
                }
            }
        }

        timelinePosition = newTimelinePos;


    }


    public void emitLeft()
    {
        trainSteamController.Cloud_Call_Left();
    }

    public void emitRight()
    {
        trainSteamController.Cloud_Call_Right();
    }

    public bool checkBeatHit(int hand)
    {
        float time = Mathf.Repeat((float)timelinePosition / 1000, 4);

        //parse list from string
        string[] lhArray = leftHandRhythms[rhythmIndex].Split(',');
        string[] rhArray = rightHandRhythms[rhythmIndex].Split(',');

        if (hand == 0)
        {
            //check left hand hit a beat
            if (currentLeftBeat != -1)
            {
                if (currentLeftBeat != leftBeatIgnore)
                {
                    Debug.Log("Hit Left Beat, Time: " + time + ", Beat: " + currentLeftBeat);
                    leftBeatIgnore = currentLeftBeat;
                    return true;
                }
            }
        }
        else
        {
            //check right hand hit a beat
            if (currentRightBeat != -1)
            {
                if (currentRightBeat != rightBeatIgnore)
                {
                    Debug.Log("Hit Right Beat, Time: " + time + ", Beat: " + currentRightBeat);
                    rightBeatIgnore = currentRightBeat;
                    return true;
                }
            }
        }


        return false;
    }
}
