using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class PressurePlateTwoDoor : MonoBehaviour
{
    public GameObject door;
    public GameObject door2;
    bool onPressurePlate = false;

    void Update()
    {
        while (onPressurePlate && door.transform.position.y <= 2.6 && door2.transform.position.y <= 2.6)
        {
            door.transform.position += new Vector3(0, 5, 0);
            //door2.transform.position += new Vector3(0, 5, 0);
            door2.transform.position = new Vector3(door2.transform.position.x, door2.transform.position.y + (float)5, door2.transform.position.z);
            //Just set position
            door2.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                door2.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(door2.transform.position);
            });
        }

        while (!onPressurePlate && door.transform.position.y > 2.5 && door2.transform.position.y > 2.5)
        {
            door.transform.position += new Vector3(0, -5, 0);
            //door2.transform.position += new Vector3(0, -5, 0);
            door2.transform.position = new Vector3(door2.transform.position.x, door2.transform.position.y - (float)5, door2.transform.position.z);
            //Just set position
            door2.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                door2.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(door2.transform.position);
            });
        }

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject.transform.name);
        onPressurePlate = true;
    }

    void OnTriggerExit(Collider col)
    {
        onPressurePlate = false;
    }

}
