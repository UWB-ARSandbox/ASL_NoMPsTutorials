using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_PhysicsMaster : MonoBehaviour
{
    bool isPhysicsMaster = false;
    public bool IsPhysicsMaster
    {
        get { return isPhysicsMaster; }
    }

    void Awake()
    {
        DeterminePhysicsMaster();
    }

    private void Start()
    {
        ASL_PhysicsMaster[] physicsMasters = FindObjectsOfType<ASL_PhysicsMaster>();
        if (physicsMasters.Length != 1)
        {
            string errorMessage = "There cannot be more than one PhysicsMaster in the scene:";
            foreach (ASL_PhysicsMaster physicsMaster in physicsMasters)
            {
                errorMessage += " " + physicsMaster.gameObject.name;
            }
            Debug.LogError(errorMessage);
        }
    }

    public void DeterminePhysicsMaster()
    {
        if (ASL.GameLiftManager.GetInstance().AmLowestPeer())
        {
            isPhysicsMaster = true;
        }
    }

    public void UpdatePhysicsMaster()
    {
        //placeholder for if the physics master needs to be reassigned
    }
}
