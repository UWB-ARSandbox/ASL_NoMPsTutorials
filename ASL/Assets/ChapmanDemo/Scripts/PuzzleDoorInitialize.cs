using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class PuzzleDoorInitialize : MonoBehaviour
{
    public LayerMask keyLayer;
    public LayerMask doorLayer;
    public GameObject[] keys = new GameObject[10];
    private bool mazeInitialized = false;
    private bool m_isHost = false;
    private GameObject door;
    // Start is called before the first frame update
    void Start()
    {
        m_isHost = GameLiftManager.GetInstance().AmLowestPeer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isHost) return;
        if (!mazeInitialized)
        {
            InitKeys();
            mazeInitialized = true;
        }

    }

    private void InitKeys()
    {
        ASL.ASLHelper.InstantiateASLObject("PuzzleDoor", transform.position, transform.rotation, null, null, callBack);
        

        // Collider[] getKeys = Physics.OverlapSphere(transform.position, 100f, keyLayer);
        // for(int i = 0; i < getKeys.Length; i++)
        // {
        //    keys[i] = getKeys[i].transform.gameObject;
        // }
        //door.GetComponent<DoorCheck>().setKeyColors(keys);
    }

    public static void callBack(GameObject ob)
    {
        //keys.Add(ob);
    }
}
