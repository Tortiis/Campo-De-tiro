using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    private Quaternion originlocalRotation;

    private void Start()
    {
        originlocalRotation = transform.localRotation;
    }

    void Update()
    {
        updateSway();
    }
  
    private void updateSway()
    {
        float t_xLookInput = Input.GetAxis("Mouse X");
        float t_yLookInput = Input.GetAxis("Mouse Y");

        Quaternion t_xAngleAdjustament = Quaternion.AngleAxis(-t_xLookInput * 1.45f, Vector3.up);
        Quaternion t_yAngleAdjustament = Quaternion.AngleAxis(t_yLookInput * 1.45f, Vector3.right);
        Quaternion t_targetRotation = originlocalRotation * t_xAngleAdjustament * t_yAngleAdjustament;
        transform.localRotation = Quaternion.Lerp(transform.localRotation, t_targetRotation, Time.deltaTime * 10f);
    }
}
