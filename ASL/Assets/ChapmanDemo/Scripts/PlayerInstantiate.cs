using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInstantiate : MonoBehaviour
{
    private static GameObject m_playerObj = null;
    private static ASL.ASLObject m_playerAslObj = null;

    public GameObject GetPlayerGameObject()
    {
        if (m_playerObj != null)
            return m_playerObj;
        return null;
    }

    public ASL.ASLObject GetPlayerAslObject()
    {
        if (m_playerAslObj != null)
            return m_playerAslObj;
        return null;
    }

    // Take the reference of the instantiated object
    private static void StoreInstatiatedObject(GameObject obj)
    {
        m_playerObj = obj;
        m_playerAslObj = obj.GetComponent<ASL.ASLObject>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Instantiate with ASL
        ASL.ASLHelper.InstantiateASLObject("CharacterDemo", // Character Prefab
                                           Vector3.zero, // Instantiate position
                                           Quaternion.identity,
                                           null,
                                           null,
                                           StoreInstatiatedObject);
    }
  
    // Set position of the camera to gameObject
    private void UpdateCameraPosition()
    {
        Vector3 position = m_playerObj.transform.position;
        this.gameObject.transform.position = position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_playerObj == null) return;

        // Update the position of the gameobject
        UpdateCameraPosition();
    }
}
