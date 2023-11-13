using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector2 sensibility;

    private new Transform camera;
    // Start is called before the first frame update
    void Start()
    {
        camera = transform.Find("Camera");
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float hor = Input.GetAxis("Mouse X");
        float ver = Input.GetAxis("Mouse Y");
        if (hor != 0)
        {
            transform.Rotate(Vector3.up * hor * sensibility.x);
        }

        if (ver != 0)
        {
            float currentAngle = camera.localEulerAngles.x;
            float targetAngle = currentAngle - ver * sensibility.y;

            // Limit the angle to a maximum of 53 degrees
            if (targetAngle > 180)
            {
                targetAngle -= 360;
            }
            targetAngle = Mathf.Clamp(targetAngle, -53, 53);

            // Adjust for the 360 degree range
            if (targetAngle < 0)
            {
                targetAngle += 360;
            }

            camera.localEulerAngles = new Vector3(targetAngle, camera.localEulerAngles.y, camera.localEulerAngles.z);
        }
    }
}
