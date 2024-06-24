using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    CinemachineBlendListCamera blendList;

    GameObject camera1;
    GameObject camera2;
    CinemachineVirtualCameraBase vCam1;
    CinemachineVirtualCameraBase vCam2;

    private void Awake()
    {
        if (Instance != null) return;
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        blendList = GetComponent<CinemachineBlendListCamera>();

        blendList.m_Loop = false;

        camera1 = GameObject.Find("Camera1");
        camera2 = GameObject.Find("Camera2");
        vCam1 = camera1.GetComponent<CinemachineVirtualCameraBase>();
        vCam2 = camera2.GetComponent<CinemachineVirtualCameraBase>();
    }

    public void SpawnCamera()
    {
        camera1.transform.SetParent(this.transform);
        camera2.transform.SetParent(this.transform);

        blendList.m_Instructions[0].m_VirtualCamera = vCam1;
        blendList.m_Instructions[1].m_VirtualCamera = vCam2;
        blendList.m_Instructions[2].m_VirtualCamera = vCam1;
    }

}
