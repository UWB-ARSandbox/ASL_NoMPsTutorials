using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_ChangeColorOnCollision : MonoBehaviour
{
    [Tooltip("The color the player cube with turn while touching this object.")]
    public Material Color;
    [Tooltip("The color the player cube's default color.")]
    public Material PlayerOriginalColor;

    ASL_ObjectCollider m_ASLObjectCollider;
    //ASLObject m_ASLObject;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Color != null);
        Debug.Assert(PlayerOriginalColor != null);
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        Debug.Assert(m_ASLObjectCollider != null);
        //m_ASLObject = gameObject.GetComponent<ASLObject>();
        //Debug.Assert(m_ASLObject != null);
        m_ASLObjectCollider.ASL_OnCollisionEnter(ChangeColorOnCollisionEnter);
        m_ASLObjectCollider.ASL_OnCollisionExit(ChangeColorOnCollisionExit);
    }

    void ChangeColorOnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Demo_PlayerCube>() != null && 
            collision.gameObject.GetComponent<MeshRenderer>() != null)
        {
            ASLObject aSLObject = collision.gameObject.GetComponent<ASLObject>();
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndSetObjectColor(Color.color, Color.color);
            });
        }
    }

    void ChangeColorOnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Demo_PlayerCube>() != null &&
            collision.gameObject.GetComponent<MeshRenderer>() != null)
        {
            ASLObject aSLObject = collision.gameObject.GetComponent<ASLObject>();
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndSetObjectColor(PlayerOriginalColor.color, PlayerOriginalColor.color);
            });
        }
    }
}
