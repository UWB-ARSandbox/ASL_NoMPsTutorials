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
        while (onPressurePlate && door.transform.position.y <= 2.6)
        {
            door.transform.position += new Vector3(0, 5, 0);
        }

        while (!onPressurePlate && door.transform.position.y > 2.5)
        {
            door.transform.position += new Vector3(0, -5, 0);
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
