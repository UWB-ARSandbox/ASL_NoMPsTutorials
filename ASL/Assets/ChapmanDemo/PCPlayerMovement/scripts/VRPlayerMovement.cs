﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;
using System.Text;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Teleport;
public class VRPlayerMovement : MonoBehaviour, IMixedRealityTeleportHandler {


    public Transform playerMeshTransform;  //Stores playerMesh  
    public Vector3 spawnPoint = Vector3.zero;   //Base point of the player spawn point (Player will be spawned randomly within playerRandomSpwanRange from this point)
    public float movementSensitivity = 3f; //Walking sensitivity
    //public float jumpForce = 6f; //Jump functionality closed
    public LayerMask groundLayerMask; //Layer Mask for ground
    public LayerMask playerMeshLayerMask; //Layer Mask for player mesh
    public LayerMask pickAbleItemLayerMask; //Layer Mask for pickable Items
    private bool grounded; //Check if the player is on the ground 
    private bool onObject = false; //check if the player is on top of the pickable object
    private CharacterController controller; //Stores player's Character controller component
    public float gravity = -9.81f;  //Gravity to calculate falling speed
    private float fallingSpeed; //Stores falling speed
    private ASLTransformSync myASL;
    private Vector3 onObjectPos; //Player position when on the top of the pickable object
    PCPlayerItemInteraction pcPlayerItemInteraction;
    private bool spawnPointSet = false; //True if player System set its spawn point

    public bool continousMovementOn = true;
    private bool disabledLeftTeleport = false;
    Quaternion cameraRotation;
    private bool isTeleporting = false;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        pcPlayerItemInteraction = GetComponent<PCPlayerItemInteraction>();
        //calculate spawn point
        initPlayerMeshToThePoint();
        
    }

    void Update()
    {
        if (playerMeshTransform == null)
        {
            tryGettingPlayerMesh();
        }
        movePlayerMesh();
        if(continousMovementOn)
        tryUndoLeftHandTeleport();
        getCameraDirection();
        

    }

    private void getCameraDirection()
    {
        float _rotationAngle = Camera.main.transform.rotation.eulerAngles.y;
        cameraRotation = Quaternion.Euler(0, _rotationAngle, 0);
    }

    private void tryUndoLeftHandTeleport()
    {
       // if (!disabledLeftTeleport)
       // {
            GameObject leftHandTeleporter1 = GameObject.Find("Left_ParabolicTeleportPointer(Clone)");
            GameObject leftHandTeleporter2 = GameObject.Find("Left_ParabolicTeleportPointer(Clone)_Cursor");
            Debug.Log("trying getting leftHandTeleport");
            if (leftHandTeleporter1 && leftHandTeleporter2)
            {
                disabledLeftTeleport = true;
                leftHandTeleporter1.SetActive(false);
                leftHandTeleporter2.SetActive(false);
                Debug.Log("Disabled leftHandTeleporter");
            }
       // }
    }


    private void FixedUpdate()
    {
        controller.center = new Vector3(0,transform.position.y,0);

        if (spawnPointSet)
        {
            if (!isTeleporting) {
                movePlayerMovementbyJoystick();
                fallPlayer();
            }
        }
    }
    
    //This method move the player to the spawn point and instatiate playerMesh
    void initPlayerMeshToThePoint()
    {
        Vector3 randomInitPoint = new Vector3(Random.Range(-50, 50), Random.Range(-50,50), Random.Range(-50, 50));
        //controller.Move(randomInitPoint);
        transform.position = randomInitPoint;
        ASL.ASLHelper.InstantiateASLObject("PlayerMesh", randomInitPoint, Quaternion.identity);
    }
    /*
    private static void InitCallBack(GameObject _gameobject)
    {
        playerMeshTransform = _gameobject.transform;
    }*/
  
    //This will look for any playerMesh initiate and store it to the playerMeshTransform
    void tryGettingPlayerMesh()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.02f, playerMeshLayerMask);
        //Debug.Log("hitCollider has "  + hitColliders);
        if (hitColliders.Length > 0)
        {
            playerMeshTransform = hitColliders[0].transform;      
        }       
    }
   


    //This method will allow the user to move the player with their keyboard
    void movePlayerMovementbyJoystick() {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        /*rigid body movement system was changed to CharacterController system
        Vector3 movePos = transform.right * x + transform.forward * y;
        Vector3 newMovePos = new Vector3(movePos.x, playerBody.velocity.y, movePos.z);
        playerBody.velocity = newMovePos;
        transform.position = playerBody.position;*/
        Vector3 move = transform.right * x + transform.forward * y;
        controller.Move(Quaternion.Inverse(transform.rotation) * cameraRotation * move * movementSensitivity * Time.deltaTime);        
    }

    //This method will make player to fall to the ground if the player is not on the ground
    void fallPlayer()
    {
        grounded = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), .5f, groundLayerMask);
        onObject = Physics.CheckSphere(new Vector3(transform.position.x, transform.position.y - 1, transform.position.z), .5f, pickAbleItemLayerMask);
        Debug.Log("GROUNDED: " + grounded);
        /*
        if (onObject && !pcPlayerItemInteraction.pickedUpItem)
        {
            onObjectPos = transform.position;
        }

        if (onObject && pcPlayerItemInteraction.pickedUpItem)
        {
            transform.position = new Vector3(transform.position.x, onObjectPos.y, transform.position.z);
        }*/
        if (grounded)
            fallingSpeed = 0;
        else
            fallingSpeed += gravity * Time.fixedDeltaTime;
        controller.Move(Vector3.up * fallingSpeed * Time.fixedDeltaTime);
        if (transform.position.y < -99f)
        {
            transform.position = spawnPoint;
        }
    }
    // Update is called once per frame

  
    //Move the player to where ASL is initialized
    //After ASLTransformSync is applied to ASL object, allow the user to move ASL object. 
    void movePlayerMesh()
    {
        if (playerMeshTransform != null)
        {
            if (!playerMeshTransform.GetComponent<ASLPlayerSync>())
            {
                transform.position = playerMeshTransform.position + new Vector3(0,2,0);
                
            }
            else
            {
                spawnPointSet = true;
                playerMeshTransform.position = controller.transform.position + controller.center;
                playerMeshTransform.rotation = cameraRotation;
            }
        }
      
    }

    public void OnTeleportCanceled(TeleportEventData eventData)
    {
        Debug.Log("Teleport Cancelled");
        isTeleporting = false;
        controller.transform.position = transform.position;
        controller.enabled = true;
        Debug.Log("Teleport Cancelled Ended");
    }

    public void OnTeleportCompleted(TeleportEventData eventData)
    {
        Debug.Log("Teleport Completed");
        isTeleporting = false;
        controller.transform.position = transform.position;
        controller.enabled = true;
        Debug.Log("Teleport Completed Ended");
    }

    public void OnTeleportRequest(TeleportEventData eventData)
    {
        Debug.Log("Teleport Request");
        isTeleporting = true;
        controller.enabled = false;
        Debug.Log("Teleport Request Ended");
    }

    public void OnTeleportStarted(TeleportEventData eventData)
    {
        Debug.Log("Teleport Started");
        isTeleporting = true;
        controller.enabled = false;
        Debug.Log("Teleport Started Ended");
    }
    void OnEnable()
    {
        // This is the critical call that registers this class for events. Without this
        // class's IMixedRealityTeleportHandler interface will not be called.
        CoreServices.TeleportSystem.RegisterHandler<IMixedRealityTeleportHandler>(this);
    }

    void OnDisable()
    {
        // Unregistering when disabled is important, otherwise this class will continue
        // to receive teleportation events.
        CoreServices.TeleportSystem.UnregisterHandler<IMixedRealityTeleportHandler>(this);
    }

}
