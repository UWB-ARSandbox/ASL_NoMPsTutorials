using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_AutonomousObject : MonoBehaviour
{
    public delegate void IncrementPositionDelegate(Vector3 m_AdditiveMovementAmount);
    public delegate void IncrementRotationDelegate(Quaternion m_rotationAmount);

    public int Owner;

    enum transformation { translate, rotate, scale}
    Queue<transformation> transformationQueue;
    Queue<Vector3> translateQueue;
    Queue<Quaternion> rotateQueue;
    Queue<Vector3> scaleQueue;
    bool transformationFinished = true;

    int autonomousObjectIndex;
    ASLObject m_ASLObject;
    float deltaTime = 0;
    float aslUpdateRate = 0.1f;
    public float ASL_UpdateRate
    {
        get { return aslUpdateRate; }
        set { aslUpdateRate = ASL_UpdateRate; }
    }

    bool updatePosition = false;
    bool updateRotation = false;
    bool updateScale = false;

    private void Start()
    {
        m_ASLObject = GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        autonomousObjectIndex = ASL_AutonomousObjectHandler.Instance.AddAutonomousObject(m_ASLObject);
    }

    // Update is called once per frame
    void Update()
    {
        deltaTime += Time.deltaTime;
        if (deltaTime >= aslUpdateRate)
        {
            deltaTime = 0;
            if (updateRotation)
            {
                Quaternion currentRotation = gameObject.transform.rotation;
                //ASL_AutonomousObjectHandler.Instance.SetWorldRotation(autonomousObjectIndex, currentRotation);
                m_ASLObject.SendAndSetClaim(() =>
                {
                    m_ASLObject.SendAndSetWorldRotation(currentRotation);
                });
                updateRotation = false;
                Debug.Log("Rotate Sent");
            }
        }
    }

    public void AutonomousIncrementWorldRotation(Quaternion m_RotationAmount)
    {
        //gameObject.transform.rotation *= m_RotationAmount;
        //updateRotation = true;
        ASL_AutonomousObjectHandler.Instance.IncrementWorldRotation(autonomousObjectIndex, m_RotationAmount);
        //m_ASLObject.SendAndSetClaim(() =>
        //{
        //    m_ASLObject.SendAndIncrementWorldRotation(m_RotationAmount);
        //});
    }

    public void AutonomousIncrementWorldPosition(Vector3 m_AdditiveMovementAmount)
    {
        ASL_AutonomousObjectHandler.Instance.IncrementWorldPosition(autonomousObjectIndex, m_AdditiveMovementAmount);
        //if (transformationFinished)
        //{
        //    transformationFinished = false;
        //    ASL_AutonomousObjectHandler.Instance.IncrementWorldPosition(autonomousObjectIndex, m_AdditiveMovementAmount, nextTransform);
        //}
        //else
        //{
        //    translateQueue.Enqueue(m_AdditiveMovementAmount);
        //    transformationQueue.Enqueue(transformation.translate);
        //}
    }

    public void AutonomousIncrementWorldScale(Vector3 m_AdditiveScaleAmount)
    {
        ASL_AutonomousObjectHandler.Instance.IncrementWorldScale(autonomousObjectIndex, m_AdditiveScaleAmount);
    }

    public GameObject InstantiateASLObjectPrefab(GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        return null;
        /*
        guid = ASL.ASLHelper.TestInstantiateASLObject("Demo_Coin",
                    Vector3.zero,
                    Quaternion.identity, "", "", creationFuntion,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);*/
    }

    public GameObject InstantiateASLObjectPrefab(GameObject prefab, Vector3 pos, Quaternion rotation, 
        ASLObject.ASLGameObjectCreatedCallback creationCallbackFunction, 
        ASLObject.ClaimCancelledRecoveryCallback ClaimRecoveryFunction, 
        ASLObject.FloatCallback FloatFunciton)
    {
        return null;
        /*
        guid = ASL.ASLHelper.TestInstantiateASLObject("Demo_Coin",
                    Vector3.zero,
                    Quaternion.identity, "", "", creationFuntion,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);*/
    }

    public void SetAutonomousObjectIndex(int index)
    {
        autonomousObjectIndex = index;
    }

    void nextTransform(GameObject obj)
    {
        if (transformationQueue.Count > 0)
        {
            transformation transform = transformationQueue.Dequeue();
            switch (transform)
            {
                case transformation.translate:
                    Vector3 m_AdditiveMovementAmount = translateQueue.Dequeue();
                    while (transformationQueue.Count > 0 && transformationQueue.Peek() == transformation.translate)
                    {
                        transformationQueue.Dequeue();
                        m_AdditiveMovementAmount += translateQueue.Dequeue();
                    }
                    ASL_AutonomousObjectHandler.Instance.IncrementWorldPosition(autonomousObjectIndex, m_AdditiveMovementAmount, nextTransform);
                    break;
                case transformation.rotate:
                    break;
                case transformation.scale:
                    break;
            }
        }
        else
        {
            transformationFinished = true;
        }
    }
}
