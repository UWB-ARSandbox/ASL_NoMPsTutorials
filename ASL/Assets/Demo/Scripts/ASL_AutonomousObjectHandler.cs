using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ASL_AutonomousObjectHandler : MonoBehaviour
{
    public delegate void ReturnInstantatedObjectCallback(GameObject instantiatedObject);

    private static ASL_AutonomousObjectHandler _instance;
    public static ASL_AutonomousObjectHandler Instance { get { return _instance; } }

    //User will need to remove the object from the autonoumousObjects list on delete

    List<ASLObject> autonomousObjects = new List<ASLObject>();
    ASL_PhysicsMasterSingleton physicsMaster;

    Dictionary<string, ReturnInstantatedObjectCallback> instatiatedObjects = new Dictionary<string, ReturnInstantatedObjectCallback>();
    Dictionary<string, int> instatiatedObjectOwners = new Dictionary<string, int>();

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

    private void Update()
    {
        ASLObject aSLObject;
        List<string> objectsToRemove = new List<string>();
        foreach (string guid in instatiatedObjects.Keys)
        {
            ASL.ASLHelper.m_ASLObjects.TryGetValue(guid, out aSLObject);
            if (aSLObject != null)
            {
                ASL_AutonomousObject autonomousObject = aSLObject.GetComponent<ASL_AutonomousObject>();
                if (autonomousObject == null)
                {
                    autonomousObject = aSLObject.gameObject.AddComponent<ASL_AutonomousObject>();
                }
                autonomousObject.Owner = instatiatedObjectOwners[guid];
                //autonomousObject.SetAutonomousObjectIndex(AddAutonomousObject(aSLObject));
                if (instatiatedObjects[guid] != null)
                {
                    instatiatedObjects[guid](aSLObject.gameObject);
                }
                objectsToRemove.Add(guid);
            }
        }
        foreach (string guid in objectsToRemove)
        {
            instatiatedObjects.Remove(guid);
            instatiatedObjectOwners.Remove(guid);
        }
    }

    public void InstantiateAutonomousObject(GameObject prefab)
    {
        string guid = ASL.ASLHelper.InstantiateASLObjectReturnID(prefab.name, prefab.transform.position, prefab.transform.rotation);
        instatiatedObjects.Add(guid, null);
        instatiatedObjectOwners.Add(guid, ASL.GameLiftManager.GetInstance().m_PeerId);
    }

    public void InstantiateAutonomousObject(GameObject prefab, ReturnInstantatedObjectCallback callbackFunction)
    {
        string guid = ASL.ASLHelper.InstantiateASLObjectReturnID(prefab.name, prefab.transform.position, prefab.transform.rotation);
        instatiatedObjects.Add(guid, callbackFunction);
    }

    public GameObject InstantiateAutonomousObject(GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        return null;
        /*
        guid = ASL.ASLHelper.TestInstantiateASLObject("Demo_Coin",
                    Vector3.zero,
                    Quaternion.identity, "", "", creationFuntion,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);*/
    }

    public GameObject InstantiateAutonomousObject(GameObject prefab, Vector3 pos, Quaternion rotation,
        ASLObject.ASLGameObjectCreatedCallback creationCallbackFunction,
        ASLObject.ClaimCancelledRecoveryCallback ClaimRecoveryFunction,
        ASLObject.FloatCallback FloatFunciton)
    {
        return null;
        /*
        guid = ASL.ASLHelper.TestInstantiateASLObject("Demo_Coin",
                    Vector3.zero,
                    Quaternion.identity, "", "", creationFuntion,
                        ClaimRecoveryFunction,
                        MyFloatsFunction);*/
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

    public void IncrementWorldPosition(int index, Vector3 m_AdditiveMovementAmount)
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
                Debug.LogError("Object does not exist in autonomousObjects");
            }
        }
    }

    public void IncrementWorldRotation(int index, Quaternion m_RotationAmount)
    {
        if (index < autonomousObjects.Count)
        {
            ASLObject aSLObject = autonomousObjects[index];
            if (aSLObject != null && ASL.GameLiftManager.GetInstance().m_PeerId == aSLObject.GetComponent<ASL_AutonomousObject>().Owner)
            {
                aSLObject.SendAndSetClaim(() =>
                {
                    aSLObject.SendAndIncrementWorldRotation(m_RotationAmount);
                });
            }
        }
    }

    public void IncrementWorldScale(int index, Vector3 m_AdditiveScaleAmount)
    {
        if (physicsMaster.IsPhysicsMaster && index < autonomousObjects.Count)
        {
            ASLObject aSLObject = autonomousObjects[index];
            if (aSLObject != null)
            {
                aSLObject.SendAndSetClaim(() =>
                {
                    aSLObject.SendAndIncrementWorldScale(m_AdditiveScaleAmount);
                });
            }
        }
    }

    public void IncrementWorldPosition(int index, Vector3 m_AdditiveMovementAmount, ASL.GameLiftManager.OpFunctionCallback callback)
    {

        if (physicsMaster.IsPhysicsMaster && index < autonomousObjects.Count) //this will check owner
        {
            ASLObject aSLObject = autonomousObjects[index];
            aSLObject.SendAndSetClaim(() =>
            {
                // Edited by Liwen due to new updates on callback design, this comment can be removed anytime
                aSLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount, callback);
            });
        }
    }

    public void SetWorldPosition(int index, Vector3 m_AdditiveMovementAmount)
    {
        if (physicsMaster.IsPhysicsMaster && index < autonomousObjects.Count)
        {
            ASLObject aSLObject = autonomousObjects[index];
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndSetWorldPosition(m_AdditiveMovementAmount);
            });
        }
    }

    public void SetWorldRotation(int index, Quaternion m_rotationAmount)
    {
        if (physicsMaster.IsPhysicsMaster && index < autonomousObjects.Count)
        {
            ASLObject aSLObject = autonomousObjects[index];
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndSetWorldRotation(m_rotationAmount);
            });
        }
    }


}
