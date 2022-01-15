using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL;

public class Demo_CollisionTests : MonoBehaviour
{
    [Tooltip("UI element where test results will be displayed")]
    public Text DisplayText;

    ASL_ObjectCollider m_ASLObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(DisplayText != null);
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        Debug.Assert(m_ASLObjectCollider != null);
        m_ASLObjectCollider.ASL_OnCollisionEnter(OnCollisionEnterTest);
        m_ASLObjectCollider.ASL_OnCollisionExit(OnCollisionExitTest);
        m_ASLObjectCollider.ASL_OnTriggerEnter(OnTriggerEnterTest);
        m_ASLObjectCollider.ASL_OnTriggerExit(OnTriggerExitTest);
        m_ASLObjectCollider.ASL_OnTriggerStay(OnTriggerStayTest);
    }

    void OnCollisionEnterTest(Collision collision)
    {
        DisplayText.text = "OnCollisionEnter called with " + gameObject.name + " at " + Time.time;
    }

    void OnCollisionExitTest(Collision collision)
    {
        DisplayText.text = "OnCollisionExit called with " + gameObject.name + " at " + Time.time;
    }

    void OnTriggerEnterTest(Collider other)
    {
        DisplayText.text = "OnTriggerEnter called with " + gameObject.name + " at " + Time.time;
    }

    void OnTriggerExitTest(Collider other)
    {
        DisplayText.text = "OnTriggerExit called with " + gameObject.name + " at " + Time.time;
    }

    void OnTriggerStayTest(Collider other)
    {
        DisplayText.text = "OnTriggerStay called with " + gameObject.name + " at " + Time.time;
    }
}
