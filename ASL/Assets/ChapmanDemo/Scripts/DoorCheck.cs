using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    public Color[] unlockColors = new Color[10];
    public GameObject[] keys = new GameObject[10];
    public GameObject door;
    public bool unlock = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        unlock = true;

        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i] != null)
            {
                
                if (keys[i].GetComponent<Renderer>().material.color != unlockColors[i])
                {
                    unlock = false;
                }
            }
        }

        while (unlock && door.transform.position.y <= 2.6)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + (float)5, door.transform.position.z);
            //Just set position
            door.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                door.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(door.transform.position);
            });
        }

        while (!unlock && door.transform.position.y > 2.5)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - (float)5, door.transform.position.z);
            //Just set position
            door.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                door.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(door.transform.position);
            });
        }
    }
}
