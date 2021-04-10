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
    private static GameObject maze;

    public GameObject GetCharacter() { return character1; }

    /// <param name="_gameObject">The gameobject that was created</param>
    public static void AddToStorage(GameObject _gameObject)
    {
        //An example of how we can get a handle to our object that we just created but want to use later
        maze = _gameObject;
    }

    /// <param name="_id">The id of the object who's claim was rejected</param>
    /// <param name="_cancelledCallbacks">The amount of claim callbacks that were cancelled</param>
    public static void ClaimRecoveryFunction(string _id, int _cancelledCallbacks)
    {
        Debug.Log("Aw man. My claim got rejected for my object with id: " + _id + " it had " + _cancelledCallbacks + " claim callbacks to execute.");
        //If I can't have this object, no one can. (An example of how to get the object we were unable to claim based on its ID and then perform an action). Obviously,
        //deleting the object wouldn't be very nice to whoever prevented your claim
        if (ASL.ASLHelper.m_ASLObjects.TryGetValue(_id, out ASL.ASLObject _myObject))
        {
            _myObject.GetComponent<ASL.ASLObject>().DeleteObject();
        }

    }

    /// <param name="_id"></param>
    /// <param name="_myFloats"></param>
    public static void MyFloatsFunction(string _id, float[] _myFloats)
    {
        Debug.Log("The floats that were sent are:\n");
        for (int i = 0; i < 4; i++)
        {
            Debug.Log(_myFloats[i] + "\n");
        }
    }

    private void Start()
    {
        /*
        // Instantiate the maze prefab
        ASL.ASLHelper.InstantiateASLObject("CubeVisual", // TODO: Change to maze pref in resource
                                   new Vector3(0f, 0f, 0f), // TODO: Should have a parameter object
                                   Quaternion.identity, "", "",
                                   AddToStorage,
                                   ClaimRecoveryFunction,
                                   MyFloatsFunction);
        */
    }

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
