using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPlayerItemInteraction : MonoBehaviour {

    private GameObject pickedUpItem; //Store the item that user is currently picking up
    public float pickUpDistance = 4f; //Maximum distance that allow user to pick up
    public float throwingYDirection = 0.3f; //y direction for parabola projectile angle
    public float throwingForce = 350f;  //throwing force for parabola projectile 
    public LayerMask pickableLayer; //Layer Mask for pickable items layer
    public LayerMask pickableChildLayer; //Layer Mask for pickable items layer

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("e"))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity))
            {
                OnAction onAction = hit.collider.GetComponent<OnAction>();
                if (onAction != null)
                {
                    onAction.OnUse.Invoke();
                }
            }
        }
        //pick up item
        if(Input.GetMouseButtonDown(0))
        {   if (pickedUpItem == null)
            {
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpDistance, pickableLayer))
                {
                    Debug.Log("Did Hit " + hit.transform.name);
                    pickedUpItem = hit.collider.gameObject;
                } else if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpDistance, pickableChildLayer))
                {
                    Transform t = hit.transform.parent;
                    while (t != null)
                    {
                        if ((1 << t.gameObject.layer) == pickableLayer.value)
                        {
                            Debug.Log("Did Hit " + t.name);
                            pickedUpItem = t.gameObject;
                            break;
                        }
                        t = t.parent;
                    }
                }
            } else {
                pickedUpItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                pickedUpItem = null;
            }
        }
        //throw item
        if(Input.GetMouseButtonDown(1))
        {
            if (pickedUpItem != null)
            {
                GameObject objectToThrow = pickedUpItem;
                pickedUpItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                pickedUpItem = null;
                Debug.Log("Throw!");
                objectToThrow.GetComponent<Rigidbody>().AddForce((Camera.main.transform.forward + new Vector3(0, throwingYDirection, 0))* throwingForce);
            }
        }
        
        //keep the picked item at the center
        if (pickedUpItem != null)
        {
            Vector3 cameraDirection = Camera.main.transform.forward;
            pickedUpItem.transform.position = Camera.main.transform.position + cameraDirection * 2;
        }
    }
}