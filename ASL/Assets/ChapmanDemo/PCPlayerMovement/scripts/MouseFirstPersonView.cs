using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFirstPersonView : MonoBehaviour
{
    public float mouseSensitivity = 700f;
    public Transform playerBody = null;
    float xRotation = 0f;
    private PlayerInstantiate m_playerInstantiate;
    // Start is called before the first frame update
    void Start()
    {
        m_playerInstantiate = GameObject.FindObjectOfType<PlayerInstantiate>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerBody == null)
        {
            GameObject body = m_playerInstantiate.GetPlayerGameObject();
            if (body != null)
            {
                playerBody = body.transform;
            }
        }
        else
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            float yRotation = playerBody.localRotation.eulerAngles.y;
            //Debug.Log("y Rotation: " + yRotation);
            transform.rotation = Quaternion.Euler(xRotation, yRotation, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            //this.gameObject.transform.Rotate(Vector3.up * mouseX);
            //Debug.Log("Name = " + playerBody.gameObject.name);
        }
    }
}
