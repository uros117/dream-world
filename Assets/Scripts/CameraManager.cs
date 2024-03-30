using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : MonoBehaviour
{
    public CinemachineVirtualCamera[] cameras;

    public CinemachineVirtualCamera PhantasusCamera;
    public CinemachineVirtualCamera PhobetorCamera;

    public CinemachineVirtualCamera startCamera;
    public CinemachineVirtualCamera currentCam;

    public void Start()
    {
        currentCam = startCamera;

        for (int i = 0; i < cameras.Length; i++)
        { 
	        if (cameras[i] == currentCam)
            {
                cameras[i].Priority = 20;
	        }
	        else
            {
                cameras[i].Priority = 10;
            }
        }
    }

    public void SwitchCamera(CinemachineVirtualCamera newCam)
    {
        currentCam = newCam;

        currentCam.Priority = 20;

        for (int i = 0; i < cameras.Length; i++)
        {
	        if (cameras[i] != currentCam)
            {
                cameras[i].Priority = 10; 
	        } 
	    }
    }
}
