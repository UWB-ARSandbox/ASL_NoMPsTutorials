using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserSensor : MonoBehaviour
{
 
    public UnityEvent OnBeamIn;
    public UnityEvent OnBeamOut;
    private bool laserDetectedLastFrame;
    private bool laserDetected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void LateUpdate()
    {
        if (laserDetected && !laserDetectedLastFrame)
        {
            OnBeamIn.Invoke();
        }
        if (!laserDetected && laserDetectedLastFrame)
        {
            OnBeamOut.Invoke();
        }
        laserDetectedLastFrame = laserDetected;
        laserDetected = false;
    }

    public void Trigger()
    {
        laserDetected = true;
    }
    public void TestIn() {
        GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
    }
    public void TestOut()
    {
        GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
    }

}
