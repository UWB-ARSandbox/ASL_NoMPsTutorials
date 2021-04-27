using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class PressurePlate : MonoBehaviour
{
    public GameObject door = null;
    bool onPressurePlate = false;

    void Update()
    {
        if (onPressurePlate && door.transform.position.y <= 2.6)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + (float)5, door.transform.position.z);
            //Just set position
            door.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                door.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(door.transform.position);
            });
        }
        else if(!onPressurePlate && door.transform.position.y > 2.5)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - (float)5, door.transform.position.z);
            //Just set position
            door.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                door.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(door.transform.position);
            });
        }

    }

    void OnTriggerEnter(Collider col)
    {
        onPressurePlate = true;
    }

    void OnTriggerExit(Collider col)
    {
        onPressurePlate = false;
    }

}
