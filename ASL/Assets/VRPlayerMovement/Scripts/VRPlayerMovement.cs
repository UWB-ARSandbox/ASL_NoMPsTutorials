using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRPlayerMovement : MonoBehaviour
{
    private Rigidbody playerBody;
    private Vector3 inputVector;
    public float movementSensitivity = 10f;
    public float jumpForce = 6f;
    public LayerMask layerMask;
    private bool grounded; //prevent infinite jump
    // Start is called before the first frame update
    void Start()
    {
        playerBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxisRaw("Horizontal") * movementSensitivity;
        float y = Input.GetAxisRaw("Vertical") * movementSensitivity;
        //moving
        Vector3 movePos = Camera.main.transform.right * x + Camera.main.transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x, playerBody.velocity.y, movePos.z);
        playerBody.velocity = newMovePos;
        
        //check if the player is on the ground
        grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), .7f, layerMask);
        
        //jumping 
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            playerBody.velocity = new Vector3(playerBody.velocity.x, jumpForce, playerBody.velocity.z);
        }

    }
}
