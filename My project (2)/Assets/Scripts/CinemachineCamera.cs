using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public GameObject Player;
    
    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Start()
    {
        Player = GameObject.Find("Player");
        cinemachineVirtualCamera.Follow = Player.transform;
    }
}
