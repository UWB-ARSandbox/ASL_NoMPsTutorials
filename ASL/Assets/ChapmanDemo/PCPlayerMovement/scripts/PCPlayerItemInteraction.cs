using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCPlayerItemInteraction : MonoBehaviour {
    private bool close;
    public GameObject pickedUpItem;
    public float pickUpDistance = 4f;
    public float throwingYDirection = 0.3f;
    public float throwingForce = 350f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {   if (pickedUpItem == null)
            {
                int layerMask = 1 << 7;
                RaycastHit hit;
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, pickUpDistance, layerMask))
                {
                    Debug.Log("Did Hit " + hit.transform.name);
                    pickedUpItem = hit.collider.gameObject;
                }
            } else
            {
                pickedUpItem.GetComponent<Rigidbody>().velocity = Vector3.zero;
                pickedUpItem = null;
            }
        }

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
        
        if (pickedUpItem != null)
        {
            Vector3 cameraDirection = Camera.main.transform.forward;
            pickedUpItem.transform.position = Camera.main.transform.position + cameraDirection * 2;
        }
    }
}