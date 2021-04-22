using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;
public class CubeInitializer : MonoBehaviour
{
    public GameObject[] cubePositions;
    private bool mazeInitialized = false;
    private bool m_isHost = false;
    private void Start()
    {
        m_isHost = GameLiftManager.GetInstance().AmLowestPeer();
    }
    private void Update()
    {
        if (!m_isHost) return;
        if(!mazeInitialized)
        {
            cubePositions = GameObject.FindGameObjectsWithTag("CubePosition");
            if (cubePositions.Length > 0)
            {
                InitPickableCubes();
                mazeInitialized = true;
            }     
        } 
    }

    private void InitPickableCubes()
    {
        Debug.Log("START init cube");
        foreach (GameObject ob in cubePositions)
        {
            ASL.ASLHelper.InstantiateASLObject("pickAbleCube", ob.transform.position, ob.transform.rotation, null, null, callBack);
        }

    }

    public static void callBack(GameObject ob)
    {
        Debug.Log("CUBE INIT");    
            
    }
    /*
    public static void InstantiateASLObject(
    PrimitiveType _type,
    Vector3 _position,
    Quaternion _rotation,
    string _parentID,
    string _componentAssemblyQualifiedName,
    ASLObject.ASLGameObjectCreatedCallback _aslGameObjectCreatedCallbackInfo
)*/

}
