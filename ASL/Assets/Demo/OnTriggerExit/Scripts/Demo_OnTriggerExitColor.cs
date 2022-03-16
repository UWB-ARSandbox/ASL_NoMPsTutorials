using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_OnTriggerExitColor : MonoBehaviour
{
    [Tooltip("The color the player cube with turn when it exits this object.")]
    public Material Color;

    ASL_ObjectCollider m_ASLObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Color != null);
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        Debug.Assert(m_ASLObjectCollider != null);

        //Assigning the deligate function to the ASL_ObjectCollider
        m_ASLObjectCollider.ASL_OnTriggerExit(ChangeColorOnTriggerExit);
    }

    //Delegate function called by OnCollitionExit by the ASL_ObjectCollider
    void ChangeColorOnTriggerExit(Collider other)
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
