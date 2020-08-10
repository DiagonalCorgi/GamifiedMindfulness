using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComplateProject
{
    public class CloudCall : MonoBehaviour
    {
        public ParticleSystem Cloud_Left;
        public ParticleSystem Cloud_Right;
        public bool Left_Test;
        public bool Right_Test;

        public void Update()
        {
            if (Left_Test == true)
            {
                Cloud_Call_Left();
                Left_Test = false;
            }
            if (Right_Test == true)
            {
                Cloud_Call_Right();
                Right_Test = false;
            }
        }

        public void Cloud_Call_Left()
        {
            Cloud_Left.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Cloud_Left.Play();
        }

        public void Cloud_Call_Right()
        {
            Cloud_Right.Stop(true, ParticleSystemStopBehavior.StopEmitting);
            Cloud_Right.Play();
        }
    }
}

