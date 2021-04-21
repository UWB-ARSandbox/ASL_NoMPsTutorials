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
            GetComponent<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f);
        }
    }
    public void LaserHitEnd()
    {
        activations--;
        if (activations < Threshold)
        {
            GetComponent<MeshRenderer>().material.color = new Color(1.0f, 0.0f, 0.0f);
        }
    }
}
