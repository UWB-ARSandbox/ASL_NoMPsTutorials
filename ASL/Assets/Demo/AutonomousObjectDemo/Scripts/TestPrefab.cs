using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class TestPrefab : MonoBehaviour
{
    public string guid;
    public GameObject CreatedObejct;
    public GameObject CoinPrefab;


    // Start is called before the first frame update
    void Start()
    {
        //guid = ASL.ASLHelper.InstantiateASLObjectReturnID("Demo_Coin",
        //            Vector3.zero,
        //            Quaternion.identity, "", "", creationFuntion,
        //                ClaimRecoveryFunction,
        //                MyFloatsFunction);
        //Debug.Log(guid);
    }

    bool objectCreated = false;

    // Update is called once per frame
    void Update()
    {
        ASLObject aSLObject;
        //if(!objectCreated && ASL.ASLHelper.m_ASLObjects.TryGetValue(guid, out aSLObject))
        //{
        //    CreatedObejct = aSLObject.gameObject;
        //    Debug.Log("Created Object name: " + CreatedObejct.name);
        //    objectCreated = true;
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ASL_AutonomousObjectHandler.Instance.InstantiateAutonomousObject(CoinPrefab);
        }
    }

    private static void creationFuntion(GameObject _gameObject)
    {
        //does nothing
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
