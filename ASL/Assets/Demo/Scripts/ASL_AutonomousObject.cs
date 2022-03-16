using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_AutonomousObject : MonoBehaviour
{
    public delegate void IncrementPositionDelegate(Vector3 m_AdditiveMovementAmount);
    public delegate void IncrementRotationDelegate(Quaternion m_rotationAmount);

    int owner = -1;
    public int Owner {
        get { return owner; }
        set {
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_ASLObject.SendFloatArray(new float[1] { (float)value });
            });
        }
    }

    bool translateReady = true;
    bool rotateReady = true;
    bool scaleReady = true;
    //Vector3 nextTranslate = Vector3.zero;
    //Quaternion nextRotate = Quaternion.identity;
    //Vector3 nextScale = Vector3.one;

    int autonomousObjectIndex;
    ASLObject m_ASLObject;

    private void Start()
    {
        m_ASLObject = GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        m_ASLObject._LocallySetFloatCallback(floatFunction);
        autonomousObjectIndex = ASL_AutonomousObjectHandler.Instance.AddAutonomousObject(m_ASLObject);

        if (owner == -1)
        {
            owner = ASL_PhysicsMasterSingleton.Instance.PhysicsMasterPeerID;
        }
    }

    public void AutonomousIncrementWorldPosition(Vector3 m_AdditiveMovementAmount)
    {
        if (Time.timeSinceLevelLoad > 0.1)
        {
            if (translateReady && owner == ASL.GameLiftManager.GetInstance().m_PeerId)
            {
                translateReady = false;
                ASL_AutonomousObjectHandler.Instance.IncrementWorldPosition(autonomousObjectIndex, m_AdditiveMovementAmount, translateComplete);
            }
            //else if (owner == ASL.GameLiftManager.GetInstance().m_PeerId)
            //{
            //    nextTranslate += m_AdditiveMovementAmount;
            //}
        }
    }

    public void AutonomousIncrementWorldRotation(Quaternion m_RotationAmount)
    {
        if (Time.timeSinceLevelLoad > 0.1)
        {
            if (rotateReady && owner == ASL.GameLiftManager.GetInstance().m_PeerId)
            {
                rotateReady = false;
                ASL_AutonomousObjectHandler.Instance.IncrementWorldRotation(autonomousObjectIndex, m_RotationAmount, rotateComplete);
            }
            //else if (owner == ASL.GameLiftManager.GetInstance().m_PeerId)
            //{
            //    nextRotate.eulerAngles += m_RotationAmount.eulerAngles;
            //}
        }
    }

    public void AutonomousIncrementWorldScale(Vector3 m_AdditiveScaleAmount)
    {
        if (Time.timeSinceLevelLoad > 0.1)
        {
            if (scaleReady && owner == ASL.GameLiftManager.GetInstance().m_PeerId)
            {
                scaleReady = false;
                ASL_AutonomousObjectHandler.Instance.IncrementWorldScale(autonomousObjectIndex, m_AdditiveScaleAmount, scaleComplete);
            }
            //else if (owner == ASL.GameLiftManager.GetInstance().m_PeerId)
            //{
            //    nextScale += m_AdditiveScaleAmount;
            //}
        }
    }

    public void AutonomousSetWorldPosition(Vector3 worldPosition)
    {
        if (owner == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            translateReady = false;
            ASL_AutonomousObjectHandler.Instance.SetWorldPosition(autonomousObjectIndex, worldPosition, translateComplete);
        }
    }

    public void AutonomousSetWorldRotation(Quaternion worldRotation)
    {
        if (owner == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            translateReady = false;
            ASL_AutonomousObjectHandler.Instance.SetWorldRotation(autonomousObjectIndex, worldRotation, translateComplete);
        }
    }

    public void AutonomousSetWorldScale(Vector3 worldScale)
    {
        if (owner == ASL.GameLiftManager.GetInstance().m_PeerId)
        {
            translateReady = false;
            ASL_AutonomousObjectHandler.Instance.SetWorldScale(autonomousObjectIndex, worldScale, translateComplete);
        }
    }

    void translateComplete(GameObject obj)
    {
        translateReady = true;
        //if (nextTranslate != Vector3.zero)
        //{
        //    ASL_AutonomousObjectHandler.Instance.IncrementWorldPosition(autonomousObjectIndex, nextTranslate, translateComplete);
        //    nextTranslate = Vector3.zero;
        //}
        //else
        //{
        //    translateReady = true;
        //}
    }

    void rotateComplete(GameObject obj)
    {
        rotateReady = true;
        //if (nextRotate != Quaternion.identity)
        //{
        //    ASL_AutonomousObjectHandler.Instance.IncrementWorldRotation(autonomousObjectIndex, nextRotate, rotateComplete);
        //    nextRotate = Quaternion.identity;
        //}
        //else
        //{
        //    rotateReady = true;
        //}
    }

    void scaleComplete(GameObject obj)
    {
        scaleReady = true;
        //if (nextScale != Vector3.one)
        //{
        //    ASL_AutonomousObjectHandler.Instance.IncrementWorldScale(autonomousObjectIndex, nextScale, scaleComplete);
        //    nextScale = Vector3.one;
        //}
        //else
        //{
        //    scaleReady = true;
        //}
    }

    public void SetAutonomousObjectIndex(int index)
    {
        autonomousObjectIndex = index;
    }

    void floatFunction(string _id, float[] _f)
    {
        owner = (int)_f[0];
    }
}
