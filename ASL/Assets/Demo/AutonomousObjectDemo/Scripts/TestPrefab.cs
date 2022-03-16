using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class TestPrefab : MonoBehaviour
{
    public string guid;
    public GameObject CreatedObejct;
    public GameObject CoinPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ASL_AutonomousObjectHandler.Instance.InstantiateAutonomousObject(CoinPrefab);
        }
    }
}
