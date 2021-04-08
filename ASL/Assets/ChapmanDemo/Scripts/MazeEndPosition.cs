using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeEndPosition : MonoBehaviour
{
    public GameObject character;
    private float endMazeDistance = 2f;
    private bool isMazeEnded = false;

    // Update is called once per frame
    private void Update()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        if (isMazeEnded) { return; }

        float dist = Vector3.Distance(character.transform.position, this.gameObject.transform.position);
        Debug.Log(dist);
        if (dist <= endMazeDistance)
        {
            Debug.Log("Maze passed");
            isMazeEnded = true;
        }
    }
}
