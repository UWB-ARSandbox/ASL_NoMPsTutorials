using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class MovingObject : MonoBehaviour
{
    public float MovementSpeed;

    Vector3 dir = Vector3.zero;
    ASLObject m_ASLObject;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObject = gameObject.GetComponent<ASLObject>();
        dir = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
    }

    // Update is called once per frame
    void Update()
    {
        m_ASLObject.SendAndSetClaim(() =>
        {
            Vector3 m_AdditiveMovementAmount = dir * MovementSpeed * Time.deltaTime;
            transform.position += m_AdditiveMovementAmount;
            m_ASLObject.SendAndSetWorldPosition(transform.position);
            //m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
        });
    }
}
