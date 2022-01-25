using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StressTestPhysicsMaster : MonoBehaviour
{
    public string PrefabName;
    public int BallsToSpawn = 100;

    bool isPhysicsMaster = true;
    TargetPoint[] travelPoints;
    int ballsSpawned = 0;
    float elapsedTime = 0.0f;

    public bool IsPhysicsMaster
    {
        get { return isPhysicsMaster; }
    }

    private void Start()
    {
        travelPoints = FindObjectsOfType<TargetPoint>();
    }

    private void Update()
    {
        if (ballsSpawned < BallsToSpawn)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 0.5f)
            {
                ASL.ASLHelper.InstantiateASLObject(PrefabName, Vector3.zero, Quaternion.identity);
                ballsSpawned++;
                elapsedTime = 0.0f;
            }
        }
    }
}
