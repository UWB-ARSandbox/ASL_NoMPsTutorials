using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL;

public class Platformer_GameManager : MonoBehaviour
{
    public Vector3 RespawnPoint;
    public Text CoinCount;
    public Text WinText;
    
    
    Dictionary<int, string> players;
    int playerIndex = 0;
    List<int> playerIDs = new List<int>();

    //spawn a player object for each user
    //Set the RespawnPoint, CoinCount and WinText for the player = to stored values
    //Give refference to player to camera

    private void Start()
    {
        players = GameLiftManager.GetInstance().m_Players;
        ASL_PhysicsMasterSingleton.Instance.SetUpPhysicsMaster();
        
        if (GameLiftManager.GetInstance().AmLowestPeer())
        {
            foreach (int playerID in players.Keys)
            {
                playerIDs.Add(playerID);
        
                ASL.ASLHelper.InstantiateASLObject("Platformer_Player",
                    new Vector3(RespawnPoint.x, RespawnPoint.y + 1, RespawnPoint.z),
                    Quaternion.identity, "", "", playerSetUp,
                        ClaimRecoveryFunction,
                        Platformer_Player.MyFloatsFunction);
                //instantiate a player
                //assign player object a user
                //set up camera
                //set up refferences
                Debug.Log("Player id: " + playerID + " Name: " + players[playerID]);

            }
        }
    }

    private static void playerSetUp(GameObject _gameObject)
    {
        if (ASL_PhysicsMasterSingleton.Instance.IsPhysicsMaster)
        {
            Platformer_GameManager _this = FindObjectOfType<Platformer_GameManager>();
            int playerID = _this.playerIDs[_this.playerIndex];
            _this.playerIndex++;
            //_gameObject.GetComponent<Platformer_Player>().SetUpPlayer(playerID, _this.RespawnPoint);
            float[] m_floatArray = new float[4] { playerID, _this.RespawnPoint.x, _this.RespawnPoint.y, _this.RespawnPoint.z };
            _gameObject.GetComponent<ASLObject>().SendAndSetClaim(() =>
            {
                _gameObject.GetComponent<ASLObject>().SendFloatArray(m_floatArray);
            });
        }
    }   
    public static void ClaimRecoveryFunction(string _id, int _cancelledCallbacks)
    {
        //does nothing
    }
    public static void MyFloatsFunction(string _id, float[] _myFloats)
    {
        //does nothing
    }
}
