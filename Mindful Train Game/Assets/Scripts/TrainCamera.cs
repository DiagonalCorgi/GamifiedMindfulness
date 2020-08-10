using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainCamera : MonoBehaviour
{

    Animator m_Animator;
    GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Camera");
        m_Animator = mainCamera.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collider Hit");
        if (other.tag == "TunnelEnterance")
        {
            m_Animator.SetBool("TunnelEnter", true);
        }
        else if (other.tag == "TunnelExit")
        {
            m_Animator.SetBool("TunnelEnter", false);
        }
    }
}
