using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_ObjectCollider : MonoBehaviour
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

    [Tooltip("Collider attached to this GameObject")]
    public Collider ObjectCollider;

    /// <summary>Reference to the PhysicsMaster in the scene. There should never be more than one PhysicsMaster per client</summary>
    ASL_PhysicsMaster physicsMaster;

    private void Start()
    {
        physicsMaster = FindObjectOfType<ASL_PhysicsMaster>();
        Debug.Assert(physicsMaster != null);
        if (ObjectCollider == null)
        {
            ObjectCollider = GetComponent<Collider>();
            Debug.Assert(ObjectCollider != null);
        }
    }

    /// <summary>
    /// Called when object enters the collision of another object. Only exicutes the callback function if this client is the PhysicsMaster.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnCollisionEnterCallback != null)
        {
            ASLObject m_ASLObject = collision.gameObject.GetComponent<ASLObject>();
            m_OnCollisionEnterCallback.Invoke(collision);
        }
    }

    /// <summary>
    /// Called when object exits the collision of another object. Only exicutes the callback function if this client is the PhysicsMaster.
    /// </summary>
    private void OnCollisionExit(Collision collision)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnCollisionExitCallback != null)
        {
            ASLObject m_ASLObject = collision.gameObject.GetComponent<ASLObject>();
            m_OnCollisionExitCallback.Invoke(collision);
        }
    }

    /// <summary>
    /// Called when object enters a trigger. Only exicutes the callback function if this client is the PhysicsMaster.
    /// </summary>
    private void OnTriggerEnter(Collider other)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnTriggerEnterCallback != null)
        {
            ASLObject m_ASLObject = other.gameObject.GetComponent<ASLObject>();
            m_OnTriggerEnterCallback.Invoke(other);
        }
    }


    /// <summary>
    /// Called when object exits a trigger. Only exicutes the callback function if this client is the PhysicsMaster.
    /// </summary>
    private void OnTriggerExit(Collider other)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnTriggerExitCallback != null)
        {
            ASLObject m_ASLObject = other.gameObject.GetComponent<ASLObject>();
            m_OnTriggerExitCallback.Invoke(other);
        }
    }

    /// <summary>
    /// Called when object stays in a trigger. Only exicutes the callback function if this client is the PhysicsMaster.
    /// </summary>
    private void OnTriggerStay(Collider other)
    {
        if (physicsMaster.IsPhysicsMaster && m_OnTriggerStayCallback != null)
        {
            ASLObject m_ASLObject = other.gameObject.GetComponent<ASLObject>();
            m_OnTriggerStayCallback.Invoke(other);
        }
    }

    /// <summary>
    /// Sets the callback function for the ASL_ObjectCollider OnCollisionEnter. This does NOT claim the object.
    /// Only a single callback function is stored at a time.
    /// </summary>
    /// <param name="onCollisionCallback">Function to be called OnCollisionEnter for this ASL_ObjectCollider. This must be a void
    /// function that takes a Collision as a paramater.</param>
    /// <returns>Returns true if the client is the PhysicsMaster. Otherwise returns false and prints a warning to the log.</returns>
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

    /// <summary>
    /// Sets the callback function for the ASL_ObjectCollider OnCollisionExit. This does NOT claim the object.
    /// Only a single callback function is stored at a time.
    /// </summary>
    /// <param name="onCollisionCallback">Function to be called OnCollisionExit for this ASL_ObjectCollider. This must be a void
    /// function that takes a Collision as a paramater.</param>
    /// <returns>Returns true if the client is the PhysicsMaster. Otherwise returns false and prints a warning to the log.</returns>
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

    /// <summary>
    /// Sets the callback function for the ASL_ObjectCollider OnTriggerEnter. This does NOT claim the object.
    /// Only a single callback function is stored at a time.
    /// </summary>
    /// <param name="onTriggerCallback">Function to be called OnTriggerEnter for this ASL_ObjectCollider. This must be a void
    /// function that takes a Collider as a paramater.</param>
    /// <returns>Returns true if the client is the PhysicsMaster. Otherwise returns false and prints a warning to the log.</returns>
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

    /// <summary>
    /// Sets the callback function for the ASL_ObjectCollider OnTriggerExit. This does NOT claim the object.
    /// Only a single callback function is stored at a time.
    /// </summary>
    /// <param name="onTriggerCallback">Function to be called OnTriggerExit for this ASL_ObjectCollider. This must be a void
    /// function that takes a Collider as a paramater.</param>
    /// <returns>Returns true if the client is the PhysicsMaster. Otherwise returns false and prints a warning to the log.</returns>
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


    /// <summary>
    /// Sets the callback function for the ASL_ObjectCollider OnTriggerStay. This does NOT claim the object.
    /// Only a single callback function is stored at a time.
    /// </summary>
    /// <param name="onTriggerCallback">Function to be called OnTriggerStay for this ASL_ObjectCollider. This must be a void
    /// function that takes a Collider as a paramater.</param>
    /// <returns>Returns true if the client is the PhysicsMaster. Otherwise returns false and prints a warning to the log.</returns>
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