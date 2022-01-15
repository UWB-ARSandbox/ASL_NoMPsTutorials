using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;
using UnityEngine.UI;

public class Demo_OnTriggerStayCountDown : MonoBehaviour
{
    [Tooltip("The counter that records how long the player has been in the trigger zone.")]
    public Text Counter;

    ASL_ObjectCollider m_ASLObjectCollider;
    ASLObject m_ASLObject;
    float count = 0;
    bool inTrigger = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(Counter != null);
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        Debug.Assert(m_ASLObjectCollider != null);
        m_ASLObject = gameObject.GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        gameObject.GetComponent<ASL.ASLObject>()._LocallySetFloatCallback(updateCounter);
        m_ASLObjectCollider.ASL_OnTriggerStay(countUpOnStay);
        m_ASLObjectCollider.ASL_OnTriggerExit(stopCounter);
    }

    private void Update()
    {
        if (inTrigger)
        {
            count += Time.deltaTime;
            //inTrigger = false;
        }
    }

    //this is only called on the PhysicsMaster
    void countUpOnStay(Collider other)
    {
        if (other.gameObject.GetComponent<Demo_PlayerCube>() != null)
        {
            m_ASLObject.SendAndSetClaim(() =>
            {
                inTrigger = true;
                float[] myArray = new float[1] { count };
                m_ASLObject.SendFloatArray(myArray);
            });
        }
    }

    void stopCounter(Collider other)
    {
        if (other.gameObject.GetComponent<Demo_PlayerCube>() != null)
        {
            m_ASLObject.SendAndSetClaim(() =>
            {
                inTrigger = false;
            });
        }
    }

    void updateCounter(string _id, float[] count)
    {
        Counter.text = "Time in Trigger: " + count[0].ToString();
    }
}
