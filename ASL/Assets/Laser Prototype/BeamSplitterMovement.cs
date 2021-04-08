using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSplitterMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position += new Vector3(0.0f, 0.0f, -0.01f);
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position += new Vector3(0.0f, 0.0f, 0.01f);
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-0.01f, 0.0f, 0f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(0.01f, 0.0f, 0f);
        }
    }
}
