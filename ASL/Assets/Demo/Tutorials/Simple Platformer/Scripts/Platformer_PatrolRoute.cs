using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Platformer_PatrolRoute : MonoBehaviour
{
    public GameObject _MovingObject;
    public float MovementSpeed = 3f;
    public Platformer_PatrolPoint[] PatrolPoints;

    int destination = 0;
    int direction = 1;

    ASLObject m_ASLObject;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(_MovingObject != null);
        m_ASLObject = _MovingObject.GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
    }

    private void FixedUpdate()
    {
        if (_MovingObject.transform.position == PatrolPoints[destination].transform.position)
        {
            if (destination == PatrolPoints.Length - 1)
            {
                direction = -1;
            }
            else if (destination == 0)
            {
                direction = 1;
            }
            destination += direction;
        }
        Vector3 m_AdditiveMovementAmount = Vector3.MoveTowards(
            _MovingObject.transform.position, 
            PatrolPoints[destination].transform.position, 
            MovementSpeed * Time.fixedDeltaTime);

        m_AdditiveMovementAmount = m_AdditiveMovementAmount - _MovingObject.transform.position;

        //_MovingObject.transform.position += m_AdditiveMovementAmount;
        m_ASLObject.SendAndSetClaim(() =>
        {
            m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
        });
        
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < PatrolPoints.Length - 1; i++)
        {
            Gizmos.color = new Color(0, 0, 0, 0.75f);
            if (PatrolPoints[i] != null && PatrolPoints[i + 1] != null)
            {
                Gizmos.DrawLine(PatrolPoints[i].transform.position, PatrolPoints[i + 1].transform.position);
            }
        }
    }
}
