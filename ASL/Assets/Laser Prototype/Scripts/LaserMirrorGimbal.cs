using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMirrorGimbal : MonoBehaviour
{

    public float Pitch;
    public float Yaw;
    public float ConstantRotationRate;
    public GameObject Stand;
    public GameObject Mirror;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ConstantRotationRate != 0f)
        {
            Pitch = Time.time * ConstantRotationRate % 360f;
            Yaw = Time.time * ConstantRotationRate % 360f;
        }
        Mirror.transform.localEulerAngles = new Vector3(Pitch, 0f, 0f);
        Stand.transform.localEulerAngles = new Vector3(0f, Yaw, 0f);
    }
}
