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
    ASL_PhysicsMasterSingleton physicsMaster;
    int playerIndex = 0;
    List<int> playerIDs = new List<int>();

    //spawn a player object for each user
    //Set the RespawnPoint, CoinCount and WinText for the player = to stored values
    //Give refference to player to camera

    private void Start()
    {
        //physicsMaster = FindObjectOfType<ASL_PhysicsMasterSingleton>();
        //players = ASL.GameLiftManager.GetInstance().m_Players;
        //
        //if (physicsMaster.IsPhysicsMaster)
        //{
        //    foreach (int playerID in players.Keys)
        //    {
        //        playerIDs.Add(playerID);
        //
        //        ASL.ASLHelper.InstantiateASLObject("Platformer_Player",
        //            new Vector3(RespawnPoint.x, RespawnPoint.y + 1, RespawnPoint.z),
        //            Quaternion.identity, "", "", playerSetUp);
        //        //instantiate a player
        //        //assign player object a user
        //        //set up camera
        //        //set up refferences
        //    }
        //}
    }

    private void playerSetUp(GameObject playerObject)
    {
        if (physicsMaster.IsPhysicsMaster)
        {
            int playerID = playerIDs[playerIndex];
            playerIndex++;
            playerObject.GetComponent<Platformer_Player>().SetUpPlayer(playerID, RespawnPoint);
        }
    }
}
