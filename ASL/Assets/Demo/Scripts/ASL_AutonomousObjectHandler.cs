using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_AutonomousObjectHandler : MonoBehaviour
{
    private static ASL_AutonomousObjectHandler _instance;
    public static ASL_AutonomousObjectHandler Instance { get { return _instance; } }

    //User will need to remove the object from the autonoumousObjects list on delete

    List<ASLObject> autonomousObjects = new List<ASLObject>();
    ASL_PhysicsMasterSingleton physicsMaster;

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

    // Start is called before the first frame update
    void Start()
    {
        physicsMaster = ASL_PhysicsMasterSingleton.Instance;
    }

    /// <summary>
    /// Adds aSLObject to the list of objects to be handled by this.
    /// </summary>
    /// <param name="aSLObject">Object to be added</param>
    /// <returns>returns index of the object. Returns -1 if the object is already being handled or if not physicsMaster.</returns>
    public int AddAutonomousObject(ASLObject aSLObject)
    {
        if (!autonomousObjects.Contains(aSLObject))
        {
            autonomousObjects.Add(aSLObject);
            return autonomousObjects.IndexOf(aSLObject);
        }
        return -1;
    }

    public void RemoveAutonomousObject(ASLObject aSLObject)
    {
        if (!autonomousObjects.Contains(aSLObject))
        {
            autonomousObjects.Remove(aSLObject);
        }
    }

    public void IncrementWorldPossision(int index, Vector3 m_AdditiveMovementAmount)
    {
        if (physicsMaster.IsPhysicsMaster && index < autonomousObjects.Count)
        {
            ASLObject aSLObject = autonomousObjects[index];
            if (aSLObject != null)
            {
                aSLObject.SendAndSetClaim(() =>
                {
                    aSLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                });
            }
            else
            {
                Debug.LogError("Object does not exist in autonomousObjecgts");
            }
        }
    }
}
