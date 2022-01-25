using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class StressTestCollider : MonoBehaviour
{
    /// <summary>Delegate for the ASL_OnCollision functions to call </summary>
    public delegate void OnCollisionCallback(Collision collision);

    /// <summary>Delegate for the ASL_OnTrigger functions to call </summary>
    public delegate void OnTriggerCallback(Collider other);

    /// <summary>Callback to be executed on OnCollisionEnter</summary>
    public OnCollisionCallback m_OnCollisionEnterCallback { get; private set; }

    /// <summary>Callback to be executed on OnCollisionExit</summary>
    public OnCollisionCallback m_OnCollisionExitCallback { get; private set; }

    /// <summary>Callback to be executed on OnTriggerEnter</summary>
    public OnTriggerCallback m_OnTriggerEnterCallback { get; private set; }

    /// <summary>Callback to be executed on OnTriggerEnter</summary>
    public OnTriggerCallback m_OnTriggerExitCallback { get; private set; }

    /// <summary>Callback to be executed on OnTriggerEnter</summary>
    public OnTriggerCallback m_OnTriggerStayCallback { get; private set; }

    public Collider ObjectCollider;

    StressTestPhysicsMaster physicsMaster;

    private void Start()
    {
        physicsMaster = FindObjectOfType<StressTestPhysicsMaster>();
        Debug.Assert(physicsMaster != null);
        if (ObjectCollider == null)
        {
            ObjectCollider = GetComponent<Collider>();
            Debug.Assert(ObjectCollider != null);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnCollisionEnterCallback != null)
        {
            ASLObject m_ASLObject = collision.gameObject.GetComponent<ASLObject>();
            m_OnCollisionEnterCallback.Invoke(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnCollisionExitCallback != null)
        {
            ASLObject m_ASLObject = collision.gameObject.GetComponent<ASLObject>();
            m_OnCollisionExitCallback.Invoke(collision);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnTriggerEnterCallback != null)
        {
            ASLObject m_ASLObject = other.gameObject.GetComponent<ASLObject>();
            m_OnTriggerEnterCallback.Invoke(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnTriggerExitCallback != null)
        {
            ASLObject m_ASLObject = other.gameObject.GetComponent<ASLObject>();
            m_OnTriggerExitCallback.Invoke(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnTriggerStayCallback != null)
        {
            ASLObject m_ASLObject = other.gameObject.GetComponent<ASLObject>();
            m_OnTriggerStayCallback.Invoke(other);
        }
    }

    public bool ASL_OnCollisionEnter(OnCollisionCallback onCollisionCallback)
    {
        if (physicsMaster.IsPhysicsMaster)
        {
            m_OnCollisionEnterCallback = onCollisionCallback;
            return true;
        }
        Debug.LogWarning("You are not the physicsMaster of this object.");
        return false;
    }

    public bool ASL_OnCollisionExit(OnCollisionCallback onCollisionCallback)
    {
        if (physicsMaster.IsPhysicsMaster)
        {
            m_OnCollisionExitCallback = onCollisionCallback;
            return true;
        }
        Debug.LogWarning("You are not the physicsMaster of this object.");
        return false;
    }

    public bool ASL_OnTriggerEnter(OnTriggerCallback onTriggerCallback)
    {
        if (physicsMaster.IsPhysicsMaster)
        {
            m_OnTriggerEnterCallback = onTriggerCallback;
            return true;
        }
        Debug.LogWarning("You are not the physicsMaster of this object.");
        return false;
    }

    public bool ASL_OnTriggerExit(OnTriggerCallback onTriggerCallback)
    {
        if (physicsMaster.IsPhysicsMaster)
        {
            m_OnTriggerExitCallback = onTriggerCallback;
            return true;
        }
        Debug.LogWarning("You are not the physicsMaster of this object.");
        return false;
    }

    public bool ASL_OnTriggerStay(OnTriggerCallback onTriggerCallback)
    {
        if (physicsMaster.IsPhysicsMaster)
        {
            m_OnTriggerStayCallback = onTriggerCallback;
            return true;
        }
        Debug.LogWarning("You are not the physicsMaster of this object.");
        return false;
    }
}
