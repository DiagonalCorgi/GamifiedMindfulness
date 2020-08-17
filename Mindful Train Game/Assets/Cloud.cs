using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public PlayerManager player;
    public int hand;
    public CloudCall cloudCall;

    public float alpha = 1;
    public float alphaPrev = 1;

    private void Start()
    {
        
    }

    private void Update()
    {
        ParticleSystem.MainModule ps = GetComponent<ParticleSystem>().main;
        Color col = ps.startColor.color;
        col.a = alpha;
        ps.startColor = col;
        if (Mathf.Abs(alpha - alphaPrev) > 0f && GetComponent<ParticleSystem>().isPlaying)
        {

            //allocate reference array
            ParticleSystem.Particle[] particles = new ParticleSystem.Particle[GetComponent<ParticleSystem>().particleCount];

            int count = GetComponent<ParticleSystem>().GetParticles(particles);
            for (int i = 0; i < count; i++)
            {
                if (particles[i].remainingLifetime > 0.2)
                {
                    particles[i].startColor = col;
                }
            }
            GetComponent<ParticleSystem>().SetParticles(particles, count);

            alphaPrev = alpha;
        }
    }

    void OnParticleCollision(GameObject other)
    {
  
        player.Hit(hand);

        if (player.transitionTimer > player.transitionTimerLength/2 && alpha <= 0.01 && GetComponent<ParticleSystem>().particleCount > 0)
        {
            cloudCall.Cloud_Left.Clear();
            cloudCall.Cloud_Right.Clear();
        }        
         
    }
}
