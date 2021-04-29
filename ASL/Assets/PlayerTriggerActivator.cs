using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTriggerActivator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("TRIGGER ENTERED!!!!!");
        if (other.GetComponent<PlayerDetectionTrigger>())
        {
            other.GetComponent<PlayerDetectionTrigger>().OnPlayerEntered.Invoke();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
