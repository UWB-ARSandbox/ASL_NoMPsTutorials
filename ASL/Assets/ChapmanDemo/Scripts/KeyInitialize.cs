using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;
public class KeyInitialize : MonoBehaviour
{
    public GameObject[] keyPositions;
    public GameObject button;
    public LayerMask keyLayer;
    private GameObject myKey;
    public static List<GameObject> keys = new List<GameObject>();
    private bool mazeInitialized = false;
    private bool m_isHost = false;
    private void Start()
    {
        m_isHost = GameLiftManager.GetInstance().AmLowestPeer();
    }
    private void Update()
    {
        if (!m_isHost) return;
        if (!mazeInitialized)
        {
            
                InitKeys();
                mazeInitialized = true;
            
        }
        if(myKey == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f, keyLayer);
            myKey = hitColliders[0].transform.gameObject;
            button.GetComponent<ButtonOneKey>().setKey(myKey);
        }
    }

    private void InitKeys()
    {
        Debug.Log("START init key");
        ASL.ASLHelper.InstantiateASLObject("Key", transform.position, transform.rotation, null, null, callBack);
    }

    public static void callBack(GameObject ob)
    {
        //keys.Add(ob);
        
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
