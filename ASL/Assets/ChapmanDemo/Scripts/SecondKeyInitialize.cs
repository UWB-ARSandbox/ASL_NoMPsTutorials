using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class SecondKeyInitialize : MonoBehaviour
{
    public GameObject keyDoor;
    private GameObject keyOne;
    private GameObject keyTwo;
    public LayerMask keyLayer;
    public GameObject button;
    private bool mazeInitialized = false;
    private bool m_isHost = false;
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
        if (keyTwo == null)
        {
            //Collider[] hitColliders = Physics.OverlapSphere(myKey.transform.position, 0.1f, keyLayer);
            //keyOne = hitColliders[0].transform.gameObject;
            Collider[] secondKey = Physics.OverlapSphere(transform.position, 0.1f, keyLayer);
            keyTwo = secondKey[0].transform.gameObject;
            //button.GetComponent<ButtonTwoKey>().setKey(keyOne);
            button.GetComponent<ButtonTwoKey>().setKey2(keyTwo);
            keyDoor.GetComponent<DoorCheck>().setKeyColors(keyTwo);
        }

    }

    private void InitKeys()
    {
        ASL.ASLHelper.InstantiateASLObject("Key", transform.position, transform.rotation, null, null, callBack);
    }

    public static void callBack(GameObject ob)
    {
        //keys.Add(ob);
    }
}
