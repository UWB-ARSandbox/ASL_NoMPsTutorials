using ASL;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformPoint : MonoBehaviour
{
    public MovingPlatformPoint Next;
    public float Speed;
    public bool SetSpeed;
    public float Delay;

    private Vector3 remotePosition;
    private Quaternion remoteRotation;
    private Vector3 remoteScale;
    private Vector3 previousPosition;
    private Quaternion previousRotation;
    private Vector3 previousScale;
    private bool transformChangedRemotely;

    private float remoteSpeed;
    private bool remoteSetSpeed;
    private float remoteDelay;
    private MovingPlatformPoint remoteNext;

    private float previousSpeed;
    private bool previousSetSpeed;
    private float previousDelay;
    private MovingPlatformPoint previousNext;

    private int id;
    private int lockHolder;
    private bool madeInitialClaim;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<ASLObject>()._LocallySetFloatCallback((string _id, float[] f) =>
        {
            lockHolder = id;
            if (id != f[0])
            {
                remotePosition = new Vector3(f[1], f[2], f[3]);
                remoteRotation = new Quaternion(f[4], f[5], f[6], f[7]);
                remoteScale = new Vector3(f[8], f[9], f[10]);
                remoteSpeed = f[11];
                remoteSetSpeed = f[12] == 1.0f;
                remoteDelay = f[13];
                remoteNext = null;
                string remoteNextID = "";
                for (int i = 14; i < f.Length; ++i)
                {
                    remoteNextID += (char) f[i];
                }
                if (ASLHelper.m_ASLObjects.ContainsKey(remoteNextID))
                {
                    remoteNext = ASLHelper.m_ASLObjects[remoteNextID].GetComponent<MovingPlatformPoint>();
                }
                transformChangedRemotely = true;
            }
        });
    }

    void FixedUpdate()
    {
        if (GetComponent<Rigidbody>() != null)
        {
            GetComponent<Rigidbody>().isKinematic = !GetComponent<ASLObject>().m_Mine;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<Rigidbody>() != null && GetComponent<Rigidbody>().isKinematic && GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshRenderer>().material.color = Color.red;
        }
        else if (GetComponent<MeshRenderer>() != null)
        {
            GetComponent<MeshRenderer>().material.color = Color.green;
        }
        GameLiftManager glm = GameLiftManager.GetInstance();
        if (glm == null)
        {
            return;
        }
        List<string> usernames = new List<string>();
        foreach (string username in glm.m_Players.Values)
        {
            usernames.Add(username);
        }
        usernames.Sort();
        id = usernames.IndexOf(glm.m_Username);

        if (id == 0 && !madeInitialClaim)
        {
            GetComponent<ASLObject>().SendAndSetClaim(() => { });
            madeInitialClaim = true;
        }
        // if transform != previousTransform then transform has changed locally, so
        //     send transform data in float array to be stored in remoteTransform
        // load remoteTransform into local transform if remoteTransform recieved
        // store transform into previousTransform

        if (transform.localPosition != previousPosition || transform.localRotation != previousRotation || transform.localScale != previousScale
            || Speed != previousSpeed || SetSpeed != previousSetSpeed || Delay != previousDelay || Next != previousNext)
        {
            // position changed locally
            GetComponent<ASLObject>().SendAndSetClaim(() =>
            {
                string NextID = Next.GetComponent<ASLObject>().m_Id;
                float[] f = new float[14 + NextID.Length];
                f[0] = id;
                f[1] = transform.localPosition.x;
                f[2] = transform.localPosition.y;
                f[3] = transform.localPosition.z;

                f[4] = transform.localRotation.x;
                f[5] = transform.localRotation.y;
                f[6] = transform.localRotation.z;
                f[7] = transform.localRotation.w;

                f[8] = transform.localScale.x;
                f[9] = transform.localScale.y;
                f[10] = transform.localScale.z;

                f[11] = Speed;
                f[12] = SetSpeed ? 1.0f : 0.0f;
                f[13] = Delay;
                for (int i = 0; i < NextID.Length; ++i)
                {
                    f[14 + i] = NextID[i];
                }
                GetComponent<ASLObject>().SendFloatArray(f);
            });
        }
        if (transformChangedRemotely)
        {
            transform.localPosition = remotePosition;
            transform.localRotation = remoteRotation;
            transform.localScale = remoteScale;
            Speed = remoteSpeed;
            SetSpeed = remoteSetSpeed;
            Delay = remoteDelay;
            Next = remoteNext;
            transformChangedRemotely = false;
        }
        previousPosition = transform.localPosition;
        previousRotation = transform.localRotation;
        previousScale = transform.localScale;
        previousSpeed = Speed;
        previousSetSpeed = SetSpeed;
        previousDelay = Delay;
        previousNext = Next;
    }
}
