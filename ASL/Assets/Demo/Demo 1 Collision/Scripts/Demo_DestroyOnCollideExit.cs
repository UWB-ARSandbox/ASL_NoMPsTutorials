using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_DestroyOnCollideExit : MonoBehaviour
{
    [Tooltip("The GameObject controlled by the players.")]
    public GameObject PlayerObject;

    ASL_ObjectCollider m_ASLObjectCollider;
    ASLObject m_ASLObject;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        m_ASLObject = gameObject.GetComponent<ASLObject>();
        Debug.Assert(m_ASLObjectCollider != null);
        Debug.Assert(m_ASLObject != null);
        Debug.Assert(PlayerObject != null);
        m_ASLObjectCollider.ASL_OnCollisionExit(DestroyObjectOnExit);
    }

    void DestroyObjectOnExit(Collision collision)
    {
        if (collision.gameObject == PlayerObject)
        {
            Destroy(this.gameObject);
        }
    }
}
