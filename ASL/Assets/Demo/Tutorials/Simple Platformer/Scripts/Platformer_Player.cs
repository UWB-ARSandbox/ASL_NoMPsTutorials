using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL;

public class Platformer_Player : MonoBehaviour
{
    public float MovementSpeed = 3f;
    public float FallVelocity = 0.3f;
    public float JumpVelocity = 10f;
    public Vector3 RespawnPoint;
    public Text CoinCount;
    public Text WinText;

    Vector3 velocity = Vector3.zero;
    bool topCollision = false;
    bool bottomCollision = false;
    bool leftCollision = false;
    bool rightCollision = false;
    bool jumpRecharged = true;
    int coinsCollected = 0;
    float floor, leftWall, rightWall;
    int ownerID;

    ASLObject m_ASLObject;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObject = GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        CoinCount = FindObjectOfType<Platformer_GameManager>().CoinCount;
        WinText = FindObjectOfType<Platformer_GameManager>().WinText;
    }

    private void Update()
    {
        if (ASL.GameLiftManager.GetInstance().AmLowestPeer() && Input.GetKeyDown(KeyCode.Space) && jumpRecharged)
        {
            jumpRecharged = false;
            velocity.y += JumpVelocity;
        }
    }

    private void FixedUpdate()
    {
        if (ASL.GameLiftManager.GetInstance().AmLowestPeer())//(ASL.GameLiftManager.GetInstance().m_PeerId == ownerID)
        {

            bool hitGround = false;
            bool hitLeft = false;
            bool hitRight = false;

            float horizontalInput = Input.GetAxis("Horizontal");
            velocity.x = horizontalInput * MovementSpeed;
            if (topCollision && velocity.y < 0)
            {
                velocity.y = 0;
                hitGround = true;
            }
            else if (!topCollision)
            {
                velocity.y -= FallVelocity;
            }
            if (bottomCollision && velocity.y > 0)
            {
                velocity.y = 0;
            }
            if (leftCollision && velocity.x > 0)
            {
                velocity.x = 0;
                hitLeft = true;
            }
            if (rightCollision && velocity.x < 0)
            {
                velocity.x = 0f;
                hitRight = true;
            }
            //transform.position += velocity * Time.fixedDeltaTime;

            Vector3 m_AdditiveMovementAmount = velocity * Time.fixedDeltaTime;

            if (topCollision && hitGround)
            {
                Vector3 endPos = transform.position + m_AdditiveMovementAmount;
                endPos.y = floor;
                m_AdditiveMovementAmount = endPos - transform.position;

            }
            if (leftCollision && hitLeft)
            {
                Vector3 endPos = transform.position + m_AdditiveMovementAmount;
                endPos.x = leftWall;
                m_AdditiveMovementAmount = endPos - transform.position;
            }
            if (rightCollision && hitRight)
            {
                Vector3 endPos = transform.position + m_AdditiveMovementAmount;
                endPos.x = rightWall;
                m_AdditiveMovementAmount = endPos - transform.position;
            }
            //transform.position += m_AdditiveMovementAmount;
            m_ASLObject.SendAndSetClaim(() =>
            {
                m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
            });
        }
    }

    public void SetUpPlayer(int _owenerID, Vector3 respawnPoint)
    {
        ownerID = _owenerID;
        RespawnPoint = respawnPoint;
    }

    public void PlatformCollisionEnter(Platformer_Collider.CollisionSide side, float collisionPoint)
    {
        switch (side)
        {
            case Platformer_Collider.CollisionSide.top:
                jumpRecharged = true;
                topCollision = true;
                floor = collisionPoint + transform.localScale.y / 2;
                break;
            case Platformer_Collider.CollisionSide.bottom:
                bottomCollision = true;
                break;
            case Platformer_Collider.CollisionSide.left:
                leftCollision = true;
                leftWall = collisionPoint - transform.localScale.x / 2;
                break;
            case Platformer_Collider.CollisionSide.right:
                rightCollision = true;
                rightWall = collisionPoint + transform.localScale.x / 2;
                break;
            default:
                Debug.LogError("side was not properly defined");
                break;
        }
    }

    public void PlatformCollisionExit(Platformer_Collider.CollisionSide side)
    {
        switch (side)
        {
            case Platformer_Collider.CollisionSide.top:
                topCollision = false;
                break;
            case Platformer_Collider.CollisionSide.bottom:
                bottomCollision = false;
                break;
            case Platformer_Collider.CollisionSide.left:
                leftCollision = false;
                break;
            case Platformer_Collider.CollisionSide.right:
                rightCollision = false;
                break;
            default:
                Debug.LogError("side was not properly defined");
                break;
        }
    }

    public void StayOnPlatform(Transform platform)
    {
        transform.parent = platform;
    }

    public void ExitPlatform()
    {
        transform.parent = null;
    }

    public void KillEnemy(Platformer_Enemy enemy)
    {
        enemy.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            enemy.GetComponent<ASL.ASLObject>().DeleteObject();
        });
        //Destroy(enemy.gameObject);
        velocity.y = JumpVelocity * 0.5f;
    }

    public void ResetPlayer()
    {
        //transform.position = RespawnPoint.transform.position;
        m_ASLObject.SendAndSetClaim(() =>
        {
            m_ASLObject.SendAndSetWorldPosition(RespawnPoint);
        });
    }

    public void CollectCoin(GameObject coin)
    {
        coinsCollected++;
        CoinCount.text = "Coins Collected: " + coinsCollected;
        coin.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            coin.GetComponent<ASL.ASLObject>().DeleteObject();
        });
        //Destroy(coin);
    }

    public void EnterWinZone()
    {
        WinText.gameObject.SetActive(true);
        WinText.text = "Player 1 Wins!!!";
    }
}
