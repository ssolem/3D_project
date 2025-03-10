using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public CinemachineVirtualCamera firstPer;
    public CinemachineVirtualCamera thirdPer;
    public LayerMask player;
    public LayerMask every;
    public float cullTimePlayer;
    public float cullTimeEvery;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChangePlayerPer()
    {
        if (firstPer.gameObject.activeSelf)
        {
            firstPer.gameObject.SetActive(false);
            Invoke("CullMaskEvery", cullTimeEvery);
            return;
        }

        firstPer.gameObject.SetActive(true);
        Invoke("CullMaskPlayer", cullTimePlayer);

    }

    void CullMaskPlayer()
    {
        Camera.main.cullingMask = ~player;
    }

    void CullMaskEvery()
    {
        Camera.main.cullingMask = every;
    }
}
