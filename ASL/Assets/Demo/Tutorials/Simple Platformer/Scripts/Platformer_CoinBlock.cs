using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Platformer_CoinBlock : Platformer_Collider
{
    public GameObject Coin;
    public Material ActivatedColor;

    bool coinSpawned = false;
    ASLObject m_ASLObject;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObject = GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
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
                if (!coinSpawned && side == CollisionSide.bottom)
                {
                    spawnCoin();
                }
                player.PlatformCollisionEnter(side, transform.position.y + y);
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
            }
        }
    }

    void spawnCoin()
    {
        ASL.ASLHelper.InstantiateASLObject("Demo_Coin",
            new Vector3(transform.position.x, transform.position.y + 1, transform.position.z),
            Coin.transform.rotation);

        //Instantiate(Coin, new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), Coin.transform.rotation);
        coinSpawned = true;
        //GetComponent<MeshRenderer>().material = ActivatedColor;
        m_ASLObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            m_ASLObject.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(ActivatedColor.color, ActivatedColor.color);
        });
    }
}
