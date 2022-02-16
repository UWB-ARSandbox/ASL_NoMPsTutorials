using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platformer_MovingPlatform : Platformer_Collider
{
    ASL_ObjectCollider m_ASLObjectCollider;
    ASL_AutonomousObjectHandler autonomousObjectHandler;
    int autonomousObjectIndex;

    // Start is called before the first frame update
    void Start()
    {
        Collider collider;
        if ((collider = GetComponent<BoxCollider>()) != null)
        {
            x = ((BoxCollider)collider).size.x * transform.localScale.x / 2 + ((BoxCollider)collider).center.x;
            y = ((BoxCollider)collider).size.y * transform.localScale.y / 2 + ((BoxCollider)collider).center.y;
        }
        else if ((collider = GetComponent<SphereCollider>()) != null)
        {
            x = ((CapsuleCollider)collider).radius * transform.localScale.x / 2;
            y = ((CapsuleCollider)collider).radius * transform.localScale.y / 2;
        }
        else if ((collider = GetComponent<CapsuleCollider>()) != null)
        {
            x = ((CapsuleCollider)collider).radius * transform.localScale.x / 2;
            y = ((CapsuleCollider)collider).height * transform.localScale.y / 2;
        }
        else
        {
            Debug.LogError("Platformer_Collider object must have a BoxCollider, CapsuleCollider, or SphereCOllider");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Platformer_Player player = other.GetComponent<Platformer_Player>();
        if (player != null)
        {
            CollisionSide side = determineColissionDirection(other);
            if (side == CollisionSide.na)
            {
                Debug.LogError("Unable to determine direction of collision between: " + gameObject.name + " and " + other.name);
            }
            else
            {
                player.PlatformCollisionEnter(side, transform.position.y + y);
                if (side == CollisionSide.top)
                {
                    player.StayOnPlatform(transform);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Platformer_Player player = other.GetComponent<Platformer_Player>();
        if (player != null)
        {
            CollisionSide side = determineColissionDirection(other);
            if (side == CollisionSide.na)
            {
                Debug.LogError("Unable to determine direction of collision between: " + gameObject.name + " and " + other.name);
            }
            else
            {
                player.PlatformCollisionExit(side);
                player.ExitPlatform();
            }
        }
    }
}
