using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_CollideCreateSphere : MonoBehaviour
{
    public string PrefabName;

    ASL_ObjectCollider m_ASLObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        Debug.Assert(m_ASLObjectCollider != null);

        //Assigning the deligate function to the ASL_ObjectCollider
        m_ASLObjectCollider.ASL_OnCollisionEnter(createSphereOnCollitionEnter);
    }

    //Delegate function called by OnCollitionEnter by the ASL_ObjectCollider
    void createSphereOnCollitionEnter(Collision collision)
    {
        ASL.ASLHelper.InstantiateASLObject(PrefabName, new Vector3(0, 5, 0), Quaternion.identity);
    }
}
