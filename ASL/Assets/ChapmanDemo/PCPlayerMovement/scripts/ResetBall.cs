using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBall : MonoBehaviour
{
    public Vector3 initPos;
    public GameObject ball;
    // Start is called before the first frame update
    void Start()
    {
        initPos = ball.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 10;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100, layerMask))
            {
                if (hit.transform.name == "ButtonBallReset")
                {
                    ball.transform.position = initPos;            
                }
            
            }

        }
    }
}
