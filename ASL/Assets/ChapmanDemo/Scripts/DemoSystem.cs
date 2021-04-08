using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSystem : MonoBehaviour
{
    public GameObject character1;
    public GameObject characterCam;
    public GameObject topDownCam;
    public Transform endPosition;
    private float m_movement_dX = 100; // per second
    private float m_movement_dZ = 100; // per second
    private float m_rotation_dY = 50; // per second

    // Update is called once per frame
    private void Update()
    {
        CheckCharacterMovement();
        CheckCharacterRotation();
        CheckSwitchCamera();
    }

    private void CheckCharacterMovement()
    {
        Vector3 position = Vector3.zero;

        // Movement in X and Z direction
        if (Input.GetKey(KeyCode.W))
        {
            // Decrease in X
            //Debug.Log("W is pressed");
            position.x += (-m_movement_dX * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // Decrease in Z
            //Debug.Log("A is pressed");
            position.z += (-m_movement_dZ * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // Increase in X
            //Debug.Log("S is pressed");
            position.x += (m_movement_dX * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            // Increase in Z
            //Debug.Log("D is pressed");
            position.z += (m_movement_dZ * Time.smoothDeltaTime);
        }
        //Debug.Log(position);
        Rigidbody rb = character1.GetComponent<Rigidbody>();
        rb.velocity = position;
    }

    private void CheckCharacterRotation()
    {
        Quaternion rotation = Quaternion.identity;
        // Rotation in Y
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Decrease in Y
            //Debug.Log("LeftArrow is pressed");
            rotation.y += (-m_rotation_dY * Time.smoothDeltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Increase in X
            //Debug.Log("RightArrow is pressed");
            rotation.y += (m_rotation_dY * Time.smoothDeltaTime);
        }

        character1.transform.Rotate(0f, rotation.y, 0f, Space.Self);
    }

    private void CheckSwitchCamera()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (characterCam.activeSelf is true)
            {
                topDownCam.SetActive(true);
                characterCam.SetActive(false);
            }
            else
            {
                characterCam.SetActive(true);
                topDownCam.SetActive(false);
            }
        }
    }
}
