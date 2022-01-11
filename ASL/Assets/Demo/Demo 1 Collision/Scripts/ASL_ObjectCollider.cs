using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_ObjectCollider : MonoBehaviour
{
    /// <summary>Delegate for the ASL_OnCollisionEnter to call </summary>
    public delegate void OnCollisionCallback(Collision collision);

    /// <summary>Callback to be executed on OnCollisionEnter</summary>
    public OnCollisionCallback m_OnCollisionEnterCallback { get; private set; }

    /// <summary>Callback to be executed on OnCollisionExit</summary>
    public OnCollisionCallback m_OnCollisionExitCallback { get; private set; }

    /// <summary>Callback to be executed on OnCollisionStay</summary>
    public OnCollisionCallback m_OnCollisionStayCallback { get; private set; }

    public Collider ObjectCollider;

    bool isPhysicsMaster = false;

    // Start is called before the first frame update
    void Awake()
    {
        PysicsMasterSetUp();
    }

    // Update is called once per frame
    void Update()
    {
        //PysicsMasterSetUp();
    }

    void PysicsMasterSetUp()
    {
        //If host, then isPhysicHandler = true
        if (ASL.GameLiftManager.GetInstance().AmLowestPeer())
        {
            if (!isPhysicsMaster)
            {
                ObjectCollider.enabled = true;
                isPhysicsMaster = true;
            }
        }
        else
        {
            ObjectCollider.enabled = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isPhysicsMaster && m_OnCollisionEnterCallback != null)
        {
            ASLObject m_ASLObject = collision.gameObject.GetComponent<ASLObject>();
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_OnCollisionEnterCallback.Invoke(collision);
            });
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (isPhysicsMaster && m_OnCollisionExitCallback != null)
        {
            ASLObject m_ASLObject = collision.gameObject.GetComponent<ASLObject>();
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_OnCollisionExitCallback.Invoke(collision);
            });
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (isPhysicsMaster && m_OnCollisionStayCallback != null)
        {
            ASLObject m_ASLObject = collision.gameObject.GetComponent<ASLObject>();
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_OnCollisionStayCallback.Invoke(collision);
            });
        }
    }

    public bool ASL_OnCollisionEnter(OnCollisionCallback onCollisionCallback)
    {
        if (isPhysicsMaster)
        {
            m_OnCollisionEnterCallback = onCollisionCallback;
            return true;
        }
        Debug.LogWarning("You are not the physicsMaster of this object.");
        return false;
    }

    public bool ASL_OnCollisionExit(OnCollisionCallback onCollisionCallback)
    {
        if (isPhysicsMaster)
        {
            m_OnCollisionExitCallback = onCollisionCallback;
            return true;
        }
        Debug.LogWarning("You are not the physicsMaster of this object.");
        return false;
    }

    public bool ASL_OnCollisionStay(OnCollisionCallback onCollisionCallback)
    {
        if (isPhysicsMaster)
        {
            m_OnCollisionStayCallback = onCollisionCallback;
            return true;
        }
        Debug.LogWarning("You are not the physicsMaster of this object.");
        return false;
    }
}
