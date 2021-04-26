using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class SecondKeyInitialize : MonoBehaviour
{
    private GameObject myKey;
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

    }

    private void InitKeys()
    {
        if (myKey == null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.1f, keyLayer);
            myKey = hitColliders[0].transform.gameObject;
            button.GetComponent<ButtonOneKey>().setKey(myKey);
        }
    }
}
