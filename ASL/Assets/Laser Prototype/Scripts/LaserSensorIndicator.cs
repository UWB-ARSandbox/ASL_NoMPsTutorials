using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSensorIndicator : MonoBehaviour
{

    public int Threshold;

    private int activations;

    // Start is called before the first frame update
    void Start()
    {
        activations = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LaserHitStart()
    {
        activations++;
        if (activations >= Threshold)
        {
            Debug.Log("A");
            Color c = new Color(0.0f, 1.0f, 0.0f);
            GetComponent<MeshRenderer>().material.color = c;
        }
    }
    public void LaserHitEnd()
    {
        activations--;
        if (activations < Threshold)
        {
            Debug.Log("B");
            Color c = new Color(1.0f, 0.0f, 0.0f);
            GetComponent<MeshRenderer>().material.color = c;
        }
    }
}
