using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_ASL_PlayerCube : MonoBehaviour
{
    const float MOVEMENT_SPEED = 1.0f;
    ASLObject m_ASLObject;
    bool isPhysicsMaster = false;

    // Start is called before the first frame update
    void Start()
    {
        if (ASL.GameLiftManager.GetInstance().AmLowestPeer())
        {
            isPhysicsMaster = true;
        }
        m_ASLObject = gameObject.GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        m_ASLObject._LocallySetFloatCallback(PositionFloatFunction);
    }

    private void Update()
    {
        if (ASL.GameLiftManager.GetInstance().AmLowestPeer())
        {
            isPhysicsMaster = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                m_ASLObject.SendAndSetClaim(() =>
                {
                    Vector3 m_AdditiveMovementAmount = Vector3.forward * MOVEMENT_SPEED * Time.deltaTime;
                    //m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                    float[] pos = new float[3] { m_AdditiveMovementAmount.x, m_AdditiveMovementAmount.y, m_AdditiveMovementAmount.z };
                    m_ASLObject.SendFloatArray(pos);
                });
            }
            else
            {
                m_ASLObject.SendAndSetClaim(() =>
                {
                    Vector3 m_AdditiveMovementAmount = Vector3.back * MOVEMENT_SPEED * Time.deltaTime;
                    //m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                    float[] pos = new float[3] { m_AdditiveMovementAmount.x, m_AdditiveMovementAmount.y, m_AdditiveMovementAmount.z };
                    m_ASLObject.SendFloatArray(pos);
                });
            }
        }
        if (Input.GetKey(KeyCode.RightArrow) ^ Input.GetKey(KeyCode.LeftArrow))
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                m_ASLObject.SendAndSetClaim(() =>
                {
                    Vector3 m_AdditiveMovementAmount = Vector3.right * MOVEMENT_SPEED * Time.deltaTime;
                    //m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                    float[] pos = new float[3] { m_AdditiveMovementAmount.x, m_AdditiveMovementAmount.y, m_AdditiveMovementAmount.z };
                    m_ASLObject.SendFloatArray(pos);
                });
            }
            else
            {
                m_ASLObject.SendAndSetClaim(() =>
                {
                    Vector3 m_AdditiveMovementAmount = Vector3.left * MOVEMENT_SPEED * Time.deltaTime;
                    //m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                    float[] pos = new float[3] { m_AdditiveMovementAmount.x, m_AdditiveMovementAmount.y, m_AdditiveMovementAmount.z };
                    m_ASLObject.SendFloatArray(pos);
                });
            }
        }
    }

    public static void PositionFloatFunction(string _id, float[] _pos)
    {
        ASLObject m_ASLObject;
        ASL.ASLHelper.m_ASLObjects.TryGetValue(_id, out m_ASLObject);
        if (m_ASLObject != null && m_ASLObject.gameObject.GetComponent<Demo_ASL_PlayerCube>().isPhysicsMaster)
        {
            m_ASLObject.SendAndSetClaim(() =>
            {
                Rigidbody rb = m_ASLObject.GetComponent<Rigidbody>();
                Vector3 moveToPos = new Vector3(_pos[0], _pos[1], _pos[2]);
                //m_ASLObject.transform.position += moveToPos;
                m_ASLObject.SendAndIncrementWorldPosition(moveToPos);
                //Rigidbody rb = m_ASLObject.gameObject.GetComponent<Rigidbody>();
                //rb.MovePosition(m_ASLObject.gameObject.transform.position + moveToPos);
                //m_ASLObject.SendAndSetWorldPosition(rb.transform.position);
                //m_ASLObject.SendAndSetWorldRotation(rb.transform.rotation);
                //m_ASLObject.SendAndSetWorldPosition(m_ASLObject.transform.position);
                //m_ASLObject.SendAndSetWorldRotation(m_ASLObject.transform.rotation);
            });
        }
    }
}
