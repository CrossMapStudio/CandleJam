using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Controller;

    CinemachineVirtualCamera Camera;
    private float Camera_TargetOrthoSize;
    private Vector3 Camera_TargetOffsetFollow;

    private float CameraOrthoSize_Stored;
    private Vector3 CameraOffsetFollow_Stored;

    private CinemachineTransposer Transposer;
    // Start is called before the first frame update
    
    void Awake()
    {
        Controller = this;

        Camera = GetComponent<CinemachineVirtualCamera>();
        Transposer = Camera.GetCinemachineComponent<CinemachineTransposer>();

        CameraOrthoSize_Stored = Camera.m_Lens.OrthographicSize;
        CameraOffsetFollow_Stored = Transposer.m_FollowOffset;

        Camera_TargetOrthoSize = CameraOrthoSize_Stored;
        Camera_TargetOffsetFollow = CameraOffsetFollow_Stored;
    }

    // Update is called once per frame
    void Update()
    {
        Camera.m_Lens.OrthographicSize = Mathf.Lerp(Camera.m_Lens.OrthographicSize, Camera_TargetOrthoSize, Time.deltaTime * 5f);
        Transposer.m_FollowOffset = Vector3.Lerp(Transposer.m_FollowOffset, Camera_TargetOffsetFollow, Time.deltaTime * 5f);
    }

    public void ResetCameraTargets()
    {
        Camera_TargetOrthoSize = CameraOrthoSize_Stored;
        Camera_TargetOffsetFollow = CameraOffsetFollow_Stored;
    }

    public void ChangeCameraTargets(float target, Vector3 offset)
    {
        Camera_TargetOrthoSize = target;
        Camera_TargetOffsetFollow = offset + CameraOffsetFollow_Stored;
    }
}
