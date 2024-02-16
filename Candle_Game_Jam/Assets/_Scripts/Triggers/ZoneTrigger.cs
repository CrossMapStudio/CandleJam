using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private Transform Camera_Target;
    [SerializeField] private float Ortho_Size;
    [SerializeField] private Vector3 Camera_Offset;
    [SerializeField] private string Zone_Text;

    private bool Zone_Triggered;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Zone_Triggered)
            CameraController.Controller.ChangeCamera_Target_WithPlayerHold(Camera_Target, Ortho_Size, Camera_Offset, Zone_Text);

        Zone_Triggered = true;
    }
}
