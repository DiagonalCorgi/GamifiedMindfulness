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

    [Range(0F, 1F)]
    public float beatTolerance = 0.25f;

    //Rhythms syncing
    public int timelinePosition;
    private int beatTimeout;

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
        SoundM.SetSoundParameter("Speed", audioSpeed);
        SoundM.SetSoundParameter("RhythmIndex", (float)rhythmIndex);

        int newTimelinePos;
        rhythm.getTimelinePosition(out newTimelinePos);


        beatTimeout -= (int)(Time.deltaTime * 1000);
        beatTimeout = Mathf.Max(beatTimeout, 0);

        float newTime = Mathf.Repeat((float)newTimelinePos / 1000, 4);
        float newOffsetTime = Mathf.Repeat((float)newTimelinePos / 1000 + noteDelay, 4);


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
                    Debug.Log("play left");
                    emitLeft();
                    lhIndex++;
                }

                if (newOffsetTime >= rhNextBeat && newOffsetTime <= rhNextBeat + 0.1)
                {
                    beatTimeout = 100;

                    //play release steam puff
                    Debug.Log("play right");
                    emitRight();
                    rhIndex++;
                }
            }
        }

        timelinePosition = newTimelinePos;
    }


    public void emitLeft()
    {

    }

    public void emitRight()
    {

    }

    public bool checkBeatHit(bool hand)
    {
        float time = Mathf.Repeat((float)timelinePosition / 1000, 4);

        //parse list from string
        string[] lhArray = leftHandRhythms[rhythmIndex].Split(',');
        string[] rhArray = rightHandRhythms[rhythmIndex].Split(',');

        if (!hand)
        {
            //check left hand hit a beat
            for (int i = 0; i < lhArray.Length; i++)
            {
                float beat = Mathf.Repeat(float.Parse(lhArray[i]), 4);

                if (time >= beat - beatTolerance && time <= beat + beatTolerance)
                {
                    return true;
                }
            }
        }
        else
        {
            //check right hand hit a beat
            for (int i = 0; i < rhArray.Length; i++)
            {
                float beat = Mathf.Repeat(float.Parse(rhArray[i]), 4);

                if (time >= beat - beatTolerance && time <= beat + beatTolerance)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
