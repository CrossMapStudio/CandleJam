using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{
    public static CameraController Controller;

    CinemachineVirtualCamera VC_Camera;
    CinemachineVirtualCamera VC_ActiveCamera;

    private float Camera_TargetOrthoSize;
    private Vector3 Camera_TargetOffsetFollow;

    private float CameraOrthoSize_Stored;
    private Vector3 CameraOffsetFollow_Stored;

    private CinemachineTransposer Transposer;
    // Start is called before the first frame update
    
    void Awake()
    {
        Controller = this;

        VC_Camera = GetComponent<CinemachineVirtualCamera>();
        Transposer = VC_Camera.GetCinemachineComponent<CinemachineTransposer>();

        CameraOrthoSize_Stored = VC_Camera.m_Lens.OrthographicSize;
        CameraOffsetFollow_Stored = Transposer.m_FollowOffset;

        Camera_TargetOrthoSize = CameraOrthoSize_Stored;
        Camera_TargetOffsetFollow = CameraOffsetFollow_Stored;

        if (VC_Camera.m_Follow == null)
            VC_Camera.m_Follow = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        VC_Camera.m_Lens.OrthographicSize = Mathf.Lerp(VC_Camera.m_Lens.OrthographicSize, Camera_TargetOrthoSize, Time.deltaTime * 5f);
        Transposer.m_FollowOffset = Vector3.Lerp(Transposer.m_FollowOffset, Camera_TargetOffsetFollow, Time.deltaTime * 5f);
    }

    public void ResetCamera_Player()
    {
        VC_Camera.m_Follow = GameObject.FindGameObjectWithTag("Player").transform;
        Camera_TargetOrthoSize = CameraOrthoSize_Stored;
        Camera_TargetOffsetFollow = CameraOffsetFollow_Stored;
    }

    public void ChangeCamera_Player(float target, Vector3 offset)
    {
        VC_Camera.m_Follow = GameObject.FindGameObjectWithTag("Player").transform;
        Camera_TargetOrthoSize = target;
        Camera_TargetOffsetFollow = offset + CameraOffsetFollow_Stored;
    }

    public void ChangeCamera_Target_WithPlayerHold(CinemachineVirtualCamera _Camera, string Text)
    {
        UIManager.Manager.StartCenterText(Text, ResetCamera_Player_Hold);
        PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_Hold());

        VC_ActiveCamera = _Camera;

        VC_Camera.Priority = 0;
        VC_ActiveCamera.Priority = 10;
    }

    public void ResetCamera_Player_Hold()
    {
        VC_Camera.m_Follow = GameObject.FindGameObjectWithTag("Player").transform;
        PlayerController.Player_Controller.Get_PlayerStateMachine.changeState(new Player_Movement());

        VC_Camera.Priority = 10;
        VC_ActiveCamera.Priority = 0;
    }

}
