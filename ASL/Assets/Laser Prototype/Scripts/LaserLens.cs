using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLens : MonoBehaviour
{
    public LaserSensor Sensor;
    public LaserBeamLighter Beam;
    // Start is called before the first frame update
    void Start()
    {
        Beam.ExcludeSensor = Sensor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
