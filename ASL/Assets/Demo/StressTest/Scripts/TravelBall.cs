using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class TravelBall : MonoBehaviour
{
    public float MovementSpeed = 3.0f;

    Vector3 direction;
    ASLObject m_ASLObject;
    StressTestCollider m_ASLObjectCollider;
    CollisionCounter collisionCounter;

    // Start is called before the first frame update
    void Start()
    {
        collisionCounter = FindObjectOfType<CollisionCounter>();
        Debug.Assert(collisionCounter != null);
        m_ASLObject = gameObject.GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        m_ASLObjectCollider = gameObject.GetComponent<StressTestCollider>();
        Debug.Assert(m_ASLObjectCollider != null);
        m_ASLObjectCollider.ASL_OnCollisionEnter(incrementCollisionEnterCount);
    }

    // Update is called once per frame
    void Update()
    {
        m_ASLObject.SendAndSetClaim(() =>
        {
            Vector3 m_AdditiveMovementAmount = Vector3.up * MovementSpeed * Time.deltaTime;
            m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
        });
    }

    public void AssignDestination(Vector3 targetPoint)
    {
        direction = (targetPoint - transform.position).normalized;
    }

    void incrementCollisionEnterCount(Collision collision)
    {
        collisionCounter.IncrementCount();
    }
}
