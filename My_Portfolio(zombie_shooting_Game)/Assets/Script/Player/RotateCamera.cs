using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float speed = 2.5f;
    private float mouseY;

    // Update is called once per frame
    void Update()
    {
        mouseY += Input.GetAxisRaw("Mouse Y") *speed * 1;
        mouseY = Mathf.Clamp(mouseY, -55.0f, 55.0f);

        transform.localEulerAngles = new Vector3(-mouseY, 0, 0);

    }
}
