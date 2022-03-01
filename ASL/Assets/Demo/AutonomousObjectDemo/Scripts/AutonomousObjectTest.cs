using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class AutonomousObjectTest : MonoBehaviour
{
    public float RotationSpeed = 100;
    public float MovementSpeed = 1;
    float direction = 1;

    ASL_AutonomousObject m_AutonomousObject;
    ASLObject m_ASLObject;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObject = GetComponent<ASLObject>();
        m_AutonomousObject = GetComponent<ASL_AutonomousObject>();
    }

    float delta = 0;

    private void Update()
    {
        delta += Time.deltaTime;
        Quaternion rotateAmount;
        rotateAmount = Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.up);
        m_AutonomousObject.AutonomousIncrementWorldRotation(rotateAmount);
        Vector3 movementAmount = new Vector3(0, MovementSpeed * Time.deltaTime * direction, 0);
        Vector3 scaleAmount = new Vector3(0.03f * direction, 0.03f * direction, 0.03f * direction);

        if (delta >= 0.5f)
        {
            direction *= -1;
            delta = 0;
        }
        m_AutonomousObject.AutonomousIncrementWorldPosition(movementAmount);
        m_AutonomousObject.AutonomousIncrementWorldScale(scaleAmount);

        movementAmount = new Vector3(MovementSpeed * Time.deltaTime * direction, 0, 0);
        m_AutonomousObject.AutonomousIncrementWorldPosition(movementAmount);

        //m_ASLObject.SendAndSetClaim(() =>
        //{
        //    m_ASLObject.SendAndIncrementWorldRotation(rotateAmount);
        //});
    }
}
