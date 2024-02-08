using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    private float _speed = 2.5f;
    private float _mouseY;

    // Update is called once per frame
    void Update()
    {
        _mouseY += Input.GetAxisRaw("Mouse Y") * _speed * 1 * Time.timeScale;
        _mouseY = Mathf.Clamp(_mouseY, -55.0f, 55.0f);

        transform.localEulerAngles = new Vector3(-(_mouseY), 0, 0);
    }
}
 