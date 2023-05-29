using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BossFight : MonoBehaviour
{
    CinemachineVirtualCamera vcam;

    private float time = 0f;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        vcam.m_Lens.OrthographicSize = Mathf.Lerp(5,10, time / 3);

        time += Time.deltaTime;
    }
}
