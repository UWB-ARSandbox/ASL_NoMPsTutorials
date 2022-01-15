using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_CollideCreateSphere : MonoBehaviour
{
    public string PrefabName;

    ASLObject m_ASLObject;
    ASL_ObjectCollider m_ASLObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        Debug.Assert(m_ASLObjectCollider != null);
        m_ASLObject = gameObject.GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        m_ASLObjectCollider.ASL_OnCollisionEnter(createSphereOnCollitionEnter);
    }

    void createSphereOnCollitionEnter(Collision collision)
    {
        m_ASLObject.SendAndSetClaim(() =>
        {
            ASL.ASLHelper.InstantiateASLObject(PrefabName, new Vector3(0, 5, 0), Quaternion.identity);
        });
    }
}
