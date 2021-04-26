using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPlayerItemInteraction : MonoBehaviour {

    public GameObject pickedUpItem; //Store the item that user is currently picking up
    public float pickUpDistance = 4f; //Maximum distance that allow user to pick up
    public float throwingYDirection = 0.3f; //y direction for parabola projectile angle
    public float throwingForce = 350f;  //throwing force for parabola projectile 
    public LayerMask pickableLayer; //Layer Mask for pickable items layer
    private float pickUpObjectDistance = 2f; //Distance between the player's eye and picked up item
    // Update is called once per frame
    void Update()
    {
        pickUpObjectDistance = 2f;
        //if (pickedUpItem && collidingObject)
        if (pickedUpItem)
        {
            RaycastHit hitInfo;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hitInfo, 3f, ~pickableLayer))
            {
                Debug.Log(hitInfo.collider.gameObject.name);
                pickUpObjectDistance = hitInfo.distance - .3f;
                if (pickUpObjectDistance < 0)
                    pickUpObjectDistance = 0;
            }
        }

        //pick up item
        if (Input.GetMouseButtonDown(0))
        {   if (pickedUpItem == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpDistance, pickableLayer))
                {
                    Debug.Log("Did Hit " + hit.transform.name);
                    pickedUpItem = hit.collider.gameObject;
                    pickedUpItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                }
            } else
            {
                leaveObejct();
            }
        }
        //throw item
        if(Input.GetMouseButtonDown(1))
        {
            if (pickedUpItem != null)
            {
                GameObject objectToThrow = pickedUpItem;
                leaveObejct();
                Debug.Log("Throw!");
                objectToThrow.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward + new Vector3(0, throwingYDirection, 0))* throwingForce);
            }
        }
        
        //keep the picked item at the center
        if (pickedUpItem != null)
        {
            Vector3 cameraDirection = Camera.main.transform.forward;
            pickedUpItem.transform.position = Camera.main.transform.position + cameraDirection * pickUpObjectDistance;
        }
    }
    
    private void leaveObejct()
    {
        pickedUpItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        pickedUpItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
        pickedUpItem = null;
    }
}