using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_PhysicsMasterSingleton : MonoBehaviour
{
    private static ASL_PhysicsMasterSingleton _instance;

    public static ASL_PhysicsMasterSingleton Instance { get { return _instance; } }
    private bool isPhysicsMaster = false;
    public bool IsPhysicsMaster
    {
        get { return isPhysicsMaster; }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    private void Start()
    {
        isPhysicsMaster = ASL.GameLiftManager.GetInstance().AmLowestPeer();
        DontDestroyOnLoad(gameObject);
    }

    public void SetUpPhysicsMaster()
    {
        isPhysicsMaster = ASL.GameLiftManager.GetInstance().AmLowestPeer();
    }
}