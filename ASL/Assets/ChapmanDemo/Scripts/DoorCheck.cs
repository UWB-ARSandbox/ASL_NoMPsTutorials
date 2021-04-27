using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    public Color[] unlockColors = new Color[10];
    public List<GameObject> keys = new List<GameObject>();
    public GameObject door;
    public LayerMask doorLayer;
    public bool unlock = false;
    bool test = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (test)
        {
            Collider[] Door = Physics.OverlapSphere(transform.position, 0.5f, doorLayer);
            door = Door[0].transform.gameObject;
            test = false;
        }
        
        if(keys.Count > 1)
        {
            unlock = true;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            if (keys[i] != null)
            {
                
                if (keys[i].GetComponent<Renderer>().material.color != unlockColors[i])
                {
                    unlock = false;
                }
            }
        }

        while (unlock && door.transform.position.y <= 4)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + (float)5, door.transform.position.z);
            //Just set position
            door.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                door.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(door.transform.position);
            });
        }

        while (!unlock && door.transform.position.y > 6)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y - (float)5, door.transform.position.z);
            //Just set position
            door.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                door.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(door.transform.position);
            });
        }
    }

    public void setKeyColors(GameObject input)
    {
        keys.Add(input);
    }
}
