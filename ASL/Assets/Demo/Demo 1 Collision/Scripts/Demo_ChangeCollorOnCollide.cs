using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_ChangeCollorOnCollide : MonoBehaviour
{
    [Tooltip("The color the colliding object will turn.")]
    public Material Color;

    ASL_ObjectCollider m_ASLObjectCollider;
    ASLObject m_ASLObject;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        m_ASLObject = gameObject.GetComponent<ASLObject>();
        Debug.Assert(m_ASLObjectCollider != null);
        Debug.Assert(m_ASLObject != null);
        Debug.Assert(Color != null);
        m_ASLObjectCollider.ASL_OnCollisionEnter(changeColorOnCollide);
    }

    void changeColorOnCollide(Collision collision)
    {
        collision.gameObject.GetComponent<ASLObject>()?.SendAndSetObjectColor(Color.color, Color.color);
    }
}
