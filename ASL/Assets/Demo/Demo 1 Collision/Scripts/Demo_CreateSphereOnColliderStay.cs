using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class Demo_CreateSphereOnColliderStay : MonoBehaviour
{
    [Tooltip("The GameObject controlled by the players.")]
    public GameObject PlayerObject;
    [Tooltip("The GameObject to be created.")]
    public GameObject ObjectToCreate;

    ASL_ObjectCollider m_ASLObjectCollider;
    ASLObject m_ASLObject;
    float timer = 0.0f;
    bool spawnEnabled = true;

    // Start is called before the first frame update
    void Start()
    {
        m_ASLObjectCollider = gameObject.GetComponent<ASL_ObjectCollider>();
        m_ASLObject = gameObject.GetComponent<ASLObject>();
        Debug.Assert(m_ASLObjectCollider != null);
        Debug.Assert(m_ASLObject != null);
        Debug.Assert(PlayerObject != null);
        Debug.Assert(ObjectToCreate != null);
        m_ASLObjectCollider.ASL_OnCollisionStay(CreateOnStay);
    }

    private void Update()
    {
        if (!spawnEnabled)
        {
            timer += Time.deltaTime;
            if (timer >= 2.0f)
            {
                spawnEnabled = true;
                timer = 0.0f;
            }
        }
    }

    void CreateOnStay(Collision collision)
    {
        if (spawnEnabled && collision.gameObject == PlayerObject)
        {
            Instantiate(ObjectToCreate);
            ObjectToCreate.transform.position = new Vector3(0, 3, 0);
        }
    }
}
