using BaseTemplate.Behaviours;
using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoSingleton<CameraManager>
{
    public List<Camera> allCameras;
    public void CameraSwitch(string cameraName)
    {
        foreach(Camera camera in allCameras)
        {
            if (camera.cameraName == cameraName)
            {
                camera.virtualCamera.Priority = 1;
            } else
            {
                camera.virtualCamera.Priority = 0;
            }
        }
    }
}

[Serializable]
public class Camera
{
    public string cameraName;
    public CinemachineVirtualCamera virtualCamera;
}
