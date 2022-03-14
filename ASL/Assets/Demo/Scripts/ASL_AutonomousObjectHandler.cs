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
    ASLObject m_ASLObject;

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
        //m_ASLObject = GetComponent<ASLObject>();
        //Debug.Assert(m_ASLObject != null);
        //m_ASLObject._LocallySetFloatCallback(floatFunction);
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
        instatiatedObjectOwners.Add(guid, ASL.GameLiftManager.GetInstance().m_PeerId);
    }

    public void InstantiateAutonomousObject(GameObject prefab, Vector3 pos, Quaternion rotation)
    {
        string guid = ASL.ASLHelper.InstantiateASLObjectReturnID(prefab.name, pos, rotation);
        instatiatedObjects.Add(guid, null);
        instatiatedObjectOwners.Add(guid, ASL.GameLiftManager.GetInstance().m_PeerId);
    }

    public void InstantiateAutonomousObject(GameObject prefab, Vector3 pos, Quaternion rotation, ReturnInstantatedObjectCallback callbackFunction)
    {
        string guid = ASL.ASLHelper.InstantiateASLObjectReturnID(prefab.name, pos, rotation);
        instatiatedObjects.Add(guid, callbackFunction);
        instatiatedObjectOwners.Add(guid, ASL.GameLiftManager.GetInstance().m_PeerId);
    }

    public void InstantiateAutonomousObject(GameObject prefab, Vector3 pos, Quaternion rotation,
        ASLObject.ASLGameObjectCreatedCallback creationCallbackFunction,
        ASLObject.ClaimCancelledRecoveryCallback claimRecoveryFunction,
        ASLObject.FloatCallback floatFunciton)
    {
        string guid = ASL.ASLHelper.InstantiateASLObjectReturnID(prefab.name, pos, rotation, "", "", creationCallbackFunction, claimRecoveryFunction, floatFunciton);
        instatiatedObjects.Add(guid, null);
        instatiatedObjectOwners.Add(guid, ASL.GameLiftManager.GetInstance().m_PeerId);
    }

    public void InstantiateAutonomousObject(GameObject prefab, Vector3 pos, Quaternion rotation,
        ASLObject.ASLGameObjectCreatedCallback creationCallbackFunction,
        ASLObject.ClaimCancelledRecoveryCallback claimRecoveryFunction,
        ASLObject.FloatCallback floatFunciton, ReturnInstantatedObjectCallback callbackFunction)
    {
        string guid = ASL.ASLHelper.InstantiateASLObjectReturnID(prefab.name, pos, rotation, "", "", creationCallbackFunction, claimRecoveryFunction, floatFunciton);
        instatiatedObjects.Add(guid, callbackFunction);
        instatiatedObjectOwners.Add(guid, ASL.GameLiftManager.GetInstance().m_PeerId);
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


    public void IncrementWorldRotation(int index, Quaternion m_RotationAmount)
    {
        ASLObject aSLObject = checkIfOwnerOfObject(index);
        if (aSLObject != null)
        {
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndIncrementWorldRotation(m_RotationAmount);
            });
        }
    }

    public void IncrementWorldRotation(int index, Quaternion m_RotationAmount, ASL.GameLiftManager.OpFunctionCallback callback)
    {
        ASLObject aSLObject = checkIfOwnerOfObject(index);
        if (aSLObject != null)
        {
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndIncrementWorldRotation(m_RotationAmount, callback);
            });
        }
    }

    public void IncrementWorldPosition(int index, Vector3 m_AdditiveMovementAmount)
    {
        ASLObject aSLObject = checkIfOwnerOfObject(index);
        if (aSLObject != null)
        {
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
            });
        }
    }

    public void IncrementWorldPosition(int index, Vector3 m_AdditiveMovementAmount, ASL.GameLiftManager.OpFunctionCallback callback)
    {
        ASLObject aSLObject = checkIfOwnerOfObject(index);
        if (aSLObject != null)
        {
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndIncrementWorldPosition(m_AdditiveMovementAmount, callback);
            });
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

    ASLObject checkIfOwnerOfObject(int index)
    {
        if (index < autonomousObjects.Count)
        {
            ASLObject aSLObject = autonomousObjects[index];
            if (aSLObject != null && ASL.GameLiftManager.GetInstance().m_PeerId == aSLObject.GetComponent<ASL_AutonomousObject>().Owner)
            {
                return aSLObject;
            }
        }
        return null;
    }

    public void IncrementWorldScale(int index, Vector3 m_AdditiveScaleAmount, ASL.GameLiftManager.OpFunctionCallback callback)
    {
        ASLObject aSLObject = checkIfOwnerOfObject(index);
        if (aSLObject != null)
        {
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndIncrementWorldScale(m_AdditiveScaleAmount, callback);
            });
        }
    }

    public void SetWorldPosition(int index, Vector3 WorldPosition)
    {
        ASLObject aSLObject = checkIfOwnerOfObject(index);
        if (aSLObject != null)
        {
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndSetWorldPosition(WorldPosition);
            });
        }
    }

    public void SetWorldRotation(int index, Quaternion WorldRotation)
    {
        ASLObject aSLObject = checkIfOwnerOfObject(index);
        if (aSLObject != null)
        {
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndSetWorldRotation(WorldRotation);
            });
        }
    }

    public void SetWorldScale(int index, Vector3 WorldScale)
    {
        if (physicsMaster.IsPhysicsMaster && index < autonomousObjects.Count)
        {
            ASLObject aSLObject = autonomousObjects[index];
            aSLObject.SendAndSetClaim(() =>
            {
                aSLObject.SendAndSetWorldScale(WorldScale);
            });
        }
    }
}
