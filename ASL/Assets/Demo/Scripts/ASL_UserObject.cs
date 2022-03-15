using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_UserObject : MonoBehaviour
{
    bool translateReady = true;
    bool rotateReady = true;
    bool scaleReady = true;
    int ownerID;
    ASLObject m_ASLObject;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObject = GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        m_ASLObject._LocallySetFloatCallback(floatFunction);
    }

    public bool IsOwner(int peerID)
    {
        return peerID == ownerID;
    }

    public void IncrementWorldPosition(Vector3 m_AdditiveMovementAmount)
    {
        Debug.Log("==========Function Called==========");
        if (translateReady && ownerID == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            Debug.Log("==========Able to perform translation==========");
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount, translateComplete);
            });
        }
    }

    public void IncrementWorldRotation(Quaternion m_RotationAmount)
    {
        if (rotateReady && ownerID == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_ASLObject.SendAndIncrementWorldRotation(m_RotationAmount, rotateComplete);
            });
        }
    }

    public void IncrementWorldScale(Vector3 m_AdditiveScaleAmount)
    {
        if (scaleReady && ownerID == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_ASLObject.SendAndIncrementWorldScale(m_AdditiveScaleAmount, scaleComplete);
            });
        }
    } 

    //set position/rotation/scale will not wait for current process to be completed.
    //It is concidered higher priority so will not be skipped if an ASL function has not returned yet
    public void SetWorldPosition(Vector3 worldPosition)
    {
        if (ownerID == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_ASLObject.SendAndSetWorldPosition(worldPosition, translateComplete);
            });
        }
    }

    public void SetWorldRotation(Quaternion worldRotation)
    {
        if (ownerID == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_ASLObject.SendAndSetWorldRotation(worldRotation, rotateComplete);
            });
        }
    }

    public void SetWorldScale(Vector3 worldScale)
    {
        if (ownerID == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_ASLObject.SendAndSetWorldScale(worldScale, scaleComplete);
            });
        }
    }

    void translateComplete(GameObject obj)
    {
        translateReady = true;
    }

    void rotateComplete(GameObject obj)
    {
        rotateReady = true;
    }

    void scaleComplete(GameObject obj)
    {
        scaleReady = true;
    }

    void floatFunction(string _id, float[] _f)
    {
        ownerID = (int)_f[0];
    }
}
