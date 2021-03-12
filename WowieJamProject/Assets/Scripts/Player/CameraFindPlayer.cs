using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFindPlayer : MonoBehaviour
{
    CinemachineVirtualCamera vcam;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        vcam.Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
