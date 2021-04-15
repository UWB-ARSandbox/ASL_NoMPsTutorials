using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFirstPersonView : MonoBehaviour
{
    public float mouseSensitivity = 700f;
    public float cameraHeight = 0.59f;
    public Transform Rig;
    float xRotation = 0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
                
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        //Debug.Log("mouse X? " + mouseX);
        Debug.Log("mouse Y? " + mouseY);
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Rig.Rotate(Vector3.up * mouseX);
    }
}
