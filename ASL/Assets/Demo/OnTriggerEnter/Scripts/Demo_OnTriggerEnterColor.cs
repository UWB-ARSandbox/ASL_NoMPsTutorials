using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_OnTriggerEnterColor : MonoBehaviour
{
    [Tooltip("The color the player cube with turn when it touches this object.")]
    public Material Color;

    ASL_ObjectCollider m_ASLObjectCollider;
    //ASLObject m_ASLObject;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Color != null);
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        Debug.Assert(m_ASLObjectCollider != null);
        //m_ASLObject = gameObject.GetComponent<ASLObject>();
        //Debug.Assert(m_ASLObject != null);
        m_ASLObjectCollider.ASL_OnTriggerEnter(ChangeColorOnTriggerEnter);
    }

    void ChangeColorOnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Demo_PlayerCube>() != null &&
            other.gameObject.GetComponent<MeshRenderer>() != null)
        {
            ASLObject aSLObject = other.gameObject.GetComponent<ASLObject>();
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndSetObjectColor(Color.color, Color.color);
            });
        }
    }
}
