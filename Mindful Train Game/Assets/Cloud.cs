using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public PlayerManager player;
    public int hand;
    public CloudCall cloudCall;

    private void Start()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
  
        player.Hit(hand);
        /**
         * 
         * if (player.streakCounter > player.streakLength)
        {
            cloudCall.Cloud_Left.Clear();
            cloudCall.Cloud_Right.Clear();
        }        
         */
    }
}
