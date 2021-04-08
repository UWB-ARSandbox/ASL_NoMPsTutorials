using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LaserSensor : MonoBehaviour
{
 
    public UnityEvent Sense;
    public UnityEvent Unsense;
    public UnityEvent<int> Change;
    private int lasersDetected;
    private int lasersDetectedNextFrame;
    private int inOutTrigger;

    public bool log;

    private int fixedFrame = 0;
    private uint frame = 0;
    private uint lateFrame = 0;
    private bool triggerSense;
    private bool triggerUnsense;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate()
    {
        int lasersDetectedLastFrame = lasersDetected;
        lasersDetected = lasersDetectedNextFrame;

        if (lasersDetected > 0 && lasersDetectedLastFrame == 0)
        {
            triggerSense = true;
        }
        if (lasersDetected == 0 && lasersDetectedLastFrame > 0)
        {
            triggerUnsense = true;
        }

        inOutTrigger = lasersDetected - lasersDetectedLastFrame;
        if (log)
        {
            Debug.Log("FixedUpdate(): Fixed Frame: " + fixedFrame
                + ", lasersDetectedLastFrame: " + lasersDetectedLastFrame + ", lasersDetected: " + lasersDetected);
        }

        lasersDetectedNextFrame = 0;

        fixedFrame++;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerSense)
        {
            Sense.Invoke();
            triggerSense = false;
        }

        if (triggerUnsense)
        {
            Unsense.Invoke();
            triggerUnsense = false;
        }

        if (inOutTrigger != 0)
        {
            Change.Invoke(lasersDetected);
            if (log)
            {
                Debug.Log("Update(): Frame: " + frame + " inOutTrigger: " + inOutTrigger);
            }
            inOutTrigger = 0;
        }

        frame++;
    }
    void LateUpdate()
    {
        //Debug.Log("LateUpdate(): Frame: " + frame + ", Late Frame: " + lateFrame
        //    + ", detected: " + laserDetected + ", prevDetected: " + laserDetectedLastFrame);
        lateFrame++;
        /*if (laserDetected && !laserDetectedLastFrame)
        {
            OnBeamIn.Invoke();
        }
        if (!laserDetected && laserDetectedLastFrame)
        {
            OnBeamOut.Invoke();
        }
        laserDetectedLastFrame = laserDetected;
        laserDetected = false;*/
    }

    public void Trigger()
    {
        //Debug.Log("Trigger(): Frame: " + frame + ", Late Frame: " + lateFrame
        //    + ", detected: " + laserDetected + ", prevDetected: " + laserDetectedLastFrame);
        //laserDetected = true;
        if (log)
        {
            Debug.Log("Trigger(): Fixed Frame: " + fixedFrame + ", lasersDetectedNextFrame: " + lasersDetectedNextFrame);
        }
        lasersDetectedNextFrame++;
    }

}
