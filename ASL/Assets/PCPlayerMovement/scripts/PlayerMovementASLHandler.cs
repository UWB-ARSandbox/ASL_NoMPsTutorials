using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementASLHandler : MonoBehaviour
{
    public Vector3[] initPositions;
    public GameObject pickableCube;
    public GameObject pickableSphere;
    // Start is called before the first frame update
    void Start()
    {
        ASL.ASLHelper.InstantiateASLObject("PickableCube", initPositions[0], Quaternion.identity);
        ASL.ASLHelper.InstantiateASLObject("PickableSphere", initPositions[1], Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
