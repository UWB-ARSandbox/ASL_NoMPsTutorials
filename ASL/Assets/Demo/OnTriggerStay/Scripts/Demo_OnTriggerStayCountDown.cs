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

        //Setting ASL float function
        gameObject.GetComponent<ASL.ASLObject>()._LocallySetFloatCallback(updateCounter);

        //Assigning the deligate functions to the ASL_ObjectCollider
        m_ASLObjectCollider.ASL_OnTriggerStay(countUpOnStay);
        m_ASLObjectCollider.ASL_OnTriggerExit(stopCounter);
    }

    private void Update()
    {
        if (inTrigger)
        {
            count += Time.deltaTime;
        }
    }

    /// <summary>
    /// Delegate function called by OnTriggerStay by the ASL_ObjectCollider.
    /// Sends current count to all clients. This is only exicuted by the PhysicsMaster.
    /// </summary>
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

    //Delegate function called by OnTriggerExit by the ASL_ObjectCollider
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

    //ASL float function
    void updateCounter(string _id, float[] count)
    {
        Counter.text = "Time in Trigger: " + count[0].ToString();
    }
}
