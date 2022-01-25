using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class TargetPoint : MonoBehaviour
{
    public TargetPoint[] DestinationPoints = new TargetPoint[27];
    StressTestCollider m_ASLObjectCollider;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObjectCollider = gameObject.GetComponent<StressTestCollider>();
        Debug.Assert(m_ASLObjectCollider != null);
        m_ASLObjectCollider.ASL_OnTriggerEnter(determineNewTravelPoint);
    }

    void determineNewTravelPoint(Collider other)
    {
        int rand = Random.Range(0, 26);
        other.GetComponent<TravelBall>()?.AssignDestination(DestinationPoints[rand].transform.position);
    }
}
