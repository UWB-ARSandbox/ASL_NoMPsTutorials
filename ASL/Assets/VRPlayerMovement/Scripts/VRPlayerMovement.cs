using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerMovement : MonoBehaviour
{
    private Rigidbody mixedRealityPlayspace;
    public GameObject playerBody; 
    private Vector3 inputVector;
    public float movementSensitivity = 10f;
    public float jumpForce = 6f;
    public LayerMask layerMask;
    private bool grounded; //prevent infinite jump
    public bool continousMovement = true;
    // Start is called before the first frame update
    void Start()
    {
        mixedRealityPlayspace = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        continousMovementHandler();
        float _rotationAngle = Camera.main.transform.rotation.eulerAngles.y;
        playerBody.transform.eulerAngles = new Vector3(playerBody.transform.eulerAngles.x, _rotationAngle, playerBody.transform.eulerAngles.z);
        //check if the player is on the ground
        

    }

    public void continousMovementHandler()
    {
        if (continousMovement)
        {
            float x = Input.GetAxisRaw("Horizontal") * movementSensitivity;
            float y = Input.GetAxisRaw("Vertical") * movementSensitivity;
            //moving
            Vector3 movePos = Camera.main.transform.right * x + Camera.main.transform.forward * y;
            Vector3 newMovePos = new Vector3(movePos.x, mixedRealityPlayspace.velocity.y, movePos.z);
            mixedRealityPlayspace.velocity = newMovePos;
        }
    }

    public void jump() {
        
        grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), .7f, layerMask);
        Debug.Log("jump " + grounded);
        //jumping 
        if (grounded)
        {
            mixedRealityPlayspace.velocity = new Vector3(mixedRealityPlayspace.velocity.x, jumpForce, mixedRealityPlayspace.velocity.z);
        }
    }
}
