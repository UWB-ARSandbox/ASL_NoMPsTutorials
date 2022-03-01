using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL;

public class Platformer_Coin : MonoBehaviour
{
    public float RotationSpeed = 100;

    ASL_ObjectCollider m_ObjectCollider;
    ASLObject m_ASLObject;
    ASL_AutonomousObject m_AutonomousObject;

    private void Start()
    {
        m_ASLObject = GetComponent<ASLObject>();
        m_ObjectCollider = GetComponent<ASL_ObjectCollider>();
        m_AutonomousObject = GetComponent<ASL_AutonomousObject>();
        m_ObjectCollider.ASL_OnTriggerEnter(CollideWithPlayer);
    }

    private void Update()
    {
        Quaternion rotateAmount;
        rotateAmount = Quaternion.AngleAxis(RotationSpeed * Time.deltaTime, Vector3.forward);
        m_AutonomousObject.AutonomousIncrementWorldRotation(rotateAmount);
    }

    private void CollideWithPlayer(Collider other)
    {
        Platformer_Player player = other.GetComponent<Platformer_Player>();
        if (player != null)
        {
            player.CollectCoin(gameObject);
        }
    }
}
