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
    public Vector3 RespawnPoint = new Vector3(-5, -2, 0);
    public Text CoinCount;
    public Text WinText;

    Vector3 velocity = Vector3.zero;
    bool topCollision = false;
    bool bottomCollision = false;
    bool leftCollision = false;
    bool rightCollision = false;
    bool jumpRecharged = true;
    int coinsCollected = 0;
    float floor, leftWall, rightWall; //these need to reset on triggerExit

    ASLObject m_ASLObject;
    ASL_UserObject m_UserObject;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObject = GetComponent<ASLObject>();
        Debug.Assert(m_ASLObject != null);
        CoinCount = FindObjectOfType<Platformer_GameManager>().CoinCount;
        WinText = FindObjectOfType<Platformer_GameManager>().WinText;
        if (Camera.main.GetComponent<Platformer_CameraMove>() != null)
        {
            Camera.main.GetComponent<Platformer_CameraMove>().SetUpCamera(this);
        }

        m_UserObject = GetComponent<ASL_UserObject>();
        Debug.Assert(m_UserObject != null);
    }

    private void Update()
    {
        Debug.Log("==========HIT 1==========");
        if (ASL.GameLiftManager.GetInstance().AmLowestPeer() && Input.GetKeyDown(KeyCode.Space) && jumpRecharged)
        {
            Debug.Log("==========HIT 2==========");
            jumpRecharged = false;
            velocity.y += JumpVelocity;
        }
        if (m_UserObject.IsOwner(ASL.GameLiftManager.GetInstance().m_PeerId))
        {
            Debug.Log("==========HIT 3==========");
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
            Vector3 m_AdditiveMovementAmount = velocity * Time.deltaTime;
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

            //m_ASLObject.SendAndSetClaim(() =>
            //{
            //    m_ASLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount, moveComplete);
            //});


            m_UserObject.IncrementWorldPosition(m_AdditiveMovementAmount);
        }
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
    //public static void MyFloatsFunction(string _id, float[] _myFloats)
    //{
    //    ASLObject aSLObject;
    //    ASL.ASLHelper.m_ASLObjects.TryGetValue(_id, out aSLObject);
    //    Platformer_Player player = aSLObject.gameObject.GetComponent<Platformer_Player>();
    //    player.ownerID = (int)_myFloats[0];
    //    player.RespawnPoint = new Vector3(_myFloats[1], _myFloats[2], _myFloats[3]);
    //}
}
