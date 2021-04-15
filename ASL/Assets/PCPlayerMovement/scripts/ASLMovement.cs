using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ASLMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                gameObject.GetComponent<ASL.ASLObject>().SendAndSetLocalRotation(gameObject.transform.localRotation);
            });

        gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
        {
            gameObject.GetComponent<ASL.ASLObject>().SendAndSetLocalPosition(gameObject.transform.localPosition);
        });
    }
}
