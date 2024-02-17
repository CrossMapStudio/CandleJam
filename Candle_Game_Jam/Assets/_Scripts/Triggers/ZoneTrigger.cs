using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera VC_Camera;
    [SerializeField] private string Zone_Text;

    private bool Zone_Triggered;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Zone_Triggered)
            CameraController.Controller.ChangeCamera_Target_WithPlayerHold(VC_Camera, Zone_Text);

        Zone_Triggered = true;
    }
}
