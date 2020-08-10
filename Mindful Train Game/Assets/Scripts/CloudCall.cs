using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ComplateProject
{
    public class CloudCall : MonoBehaviour
    {
        public ParticleSystem Cloud_Left;
        public ParticleSystem Cloud_Right;
        public bool test;

        public void Update()
        {
            if (test == true)
                {
                Cloud_Call_Left();
            }
        }

        public void Cloud_Call_Left()
        {
            Cloud_Left.emission.SetBursts(new[]
            {
                new ParticleSystem.Burst(0f, 10)
            });



            //GetComponent<ParticleSystem>().emission.SetBursts(new[]
            //{
            //                        new ParticleSystem.Burst(1f, 10), //float_time, short_count
            //                    });
        }
}

}

