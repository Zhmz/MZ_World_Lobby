using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CameraController : MonoBehaviour
{
    private CinemachineFreeLook m_FreelookCamera;

    void Start()
    {
        AttachCamera();
    }

    private void AttachCamera()
    {
        m_FreelookCamera = GameObject.FindObjectOfType<CinemachineFreeLook>();
        Assert.IsNotNull(m_FreelookCamera, "CameraController.AttachCamera: Couldn't find gameplay freelook camera");

        if (m_FreelookCamera)
        {
            // camera body / aim
            m_FreelookCamera.Follow = transform;
            m_FreelookCamera.LookAt = transform;
            // default rotation / zoom
            // m_MainCamera.m_Heading.m_Bias = 40f;
            // m_MainCamera.m_YAxis.Value = 0.5f;

            DontDestroyOnLoad(m_FreelookCamera);

        }
    }
}
