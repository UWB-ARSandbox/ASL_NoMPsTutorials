using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStartPosition : MonoBehaviour
{
    public GameObject character;

    // Start is called before the first frame update
    private void Start()
    {
        Vector3 position = this.gameObject.transform.position;
        position.y += 0.5f;
        character.transform.position = position;
        Debug.Log("Maze Starts");
    }
}
