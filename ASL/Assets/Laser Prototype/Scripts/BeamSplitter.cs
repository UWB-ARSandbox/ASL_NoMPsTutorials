using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamSplitter : MonoBehaviour
{
    [Serializable]
    public struct LaserModule
    {
        public GameObject Beam;
        public GameObject Sensor;
        public int N;
    }

    public List<LaserModule> Lasers;

    private int frame;

    void Start()
    {
        foreach (LaserModule m in Lasers)
        {
            m.Sensor.GetComponent<LaserSensor>().Sense.AddListener(() =>
            {
                OnBeamIn(m.Sensor);
            });
            m.Sensor.GetComponent<LaserSensor>().Unsense.AddListener(() =>
            {
                OnBeamOut(m.Sensor);
            });
        }
    }

    // Update is called once per frame
    void Update()
    {
        frame++;
        //Debug.Log("Frame: " + frame + "A: " + Lasers[0].N + " B: " + Lasers[1].N + "C: " + Lasers[2].N);
    }

    public void OnBeamIn(GameObject self)
    {
        for (int i = 0; i < Lasers.Count; ++i)
        {
            LaserModule m = Lasers[i];
            if (m.Sensor != self)
            {
                m.N++;
                if (m.N > 0)
                {
                    m.Beam.SetActive(true);
                }
                Lasers[i] = m;
            }
        }
    }

    public void OnBeamOut(GameObject self)
    {
        for (int i = 0; i < Lasers.Count; ++i)
        {
            LaserModule m = Lasers[i];
            if (m.Sensor != self)
            {
                m.N--;
                if (m.N <= 0)
                {
                    m.Beam.SetActive(false);
                }
                Lasers[i] = m;
            }
        }
    }
}
