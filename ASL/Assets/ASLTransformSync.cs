using ASL;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASLTransformSync : MonoBehaviour
{
    private Vector3 remotePosition;
    private Vector3 previousPosition;
    private bool transformChangedRemotely;
    private int frame;
    private float remoteFrame;
    private int id;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ASLObject>()._LocallySetFloatCallback((string _id, float[] f) =>
        {
            if (id != f[0])
            {
                remotePosition = new Vector3(f[1], f[2], f[3]);
                remoteFrame = f[4];
                Debug.Log("Recieved remote position update. Remote Frame: " + remoteFrame + " remotePosition: " + remotePosition);
                transformChangedRemotely = true;
            }
            Debug.Log("Remote ID: " + f[0]);
        });
    }

    // Update is called once per frame
    void Update()
    {
        frame++;

        GameLiftManager glm = GameLiftManager.GetInstance();
        List<string> usernames = new List<string>();
        foreach (string username in glm.m_Players.Values)
        {
            usernames.Add(username);
        }
        usernames.Sort();
        id = usernames.IndexOf(glm.m_Username);

        // if transform != previousTransform then transform has changed locally, so
        //     send transform data in float array to be stored in remoteTransform
        // load remoteTransform into local transform if remoteTransform recieved
        // store transform into previousTransform
        if (transform.localPosition.x != previousPosition.x)
        {
            //Debug.Log("Frame: " + frame + " transformChangedRemotely: " + transformChangedRemotely + "localPosition: " + transform.localPosition
            //   + " remotePosition: " + remotePosition + " previousPosition: " + previousPosition);
            // position changed locally
            GetComponent<ASLObject>().SendAndSetClaim(() =>
            {
                float[] f = new float[5];
                f[0] = id;
                f[1] = transform.localPosition.x;
                f[2] = transform.localPosition.y;
                f[3] = transform.localPosition.z;
                f[4] = frame;
                GetComponent<ASLObject>().SendFloatArray(f);
            });
        }
        if (transformChangedRemotely)
        {
            //Debug.Log("transformChangedRemotely. Remote Frame: " + remoteFrame);
            //Debug.Log("Frame: " + frame + " transformChangedRemotely: " + transformChangedRemotely + "localPosition: " + transform.localPosition
            //   + " remotePosition: " + remotePosition + " previousPosition: " + previousPosition);
            transform.localPosition = remotePosition;
            transformChangedRemotely = false;
        }
        previousPosition = transform.localPosition;
    }
}
