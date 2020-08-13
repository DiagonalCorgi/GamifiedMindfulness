using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour
{
    public PlayerManager player;
    public int hand;

    private void Start()
    {
        
    }

    void OnParticleCollision(GameObject other)
    {
        player.Hit(hand);
    }
}
