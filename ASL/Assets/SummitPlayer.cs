using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummitPlayer : MonoBehaviour
{
    public Vector3 playerSummitPos = new Vector3(0, 4, 0);
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(0.01f));
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        transform.position = playerSummitPos;
        Debug.Log("late start");
    }
}
