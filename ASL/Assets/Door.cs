using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Door : MonoBehaviour
{

    public float OpenOffset;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Raise()
    {
        GetComponent<ASLObject>().SendAndSetClaim(() =>
        {
            GetComponent<ASLObject>().SendAndIncrementLocalPosition(new Vector3(0, OpenOffset, 0));
        });
    }

    public void Lower()
    {
        GetComponent<ASLObject>().SendAndSetClaim(() =>
        {
            GetComponent<ASLObject>().SendAndIncrementLocalPosition(new Vector3(0, -OpenOffset, 0));
        });
    }
}
