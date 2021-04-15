using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStartPosition : MonoBehaviour
{
    private MazeSystem m_mazeSystem;
    [SerializeField] private float m_pos_y = 1f;
    [SerializeField] private float m_secFloorPos_y = 11.5f;

    public void PlaceCharacterInStartPos()
    {
        Vector3 pos = this.gameObject.transform.position;
        int numCharacterInMaze = m_mazeSystem.GetNumCharacterInMaze();
        for (int i = 0; i < numCharacterInMaze; i++)
        {
            GameObject character = m_mazeSystem.GetMazeCharacterByIndex(i);
            if (i % 2 == 0) // even number
            {
                pos.y = m_pos_y;
                // Set character position to bottom floor
                // TODO: add ASL
                character.transform.position = pos;
                //Debug.Log("Add Character name: " + character.name + " to bottom floor list");
                m_mazeSystem.AddBottomFloorCharac(character);
            }
            else
            {
                pos.y = m_secFloorPos_y;
                character.transform.position = pos;
                //Debug.Log("Add Character name: " + character.name + " to top floor list");
                m_mazeSystem.AddTopFloorCharac(character);
            }
        }
        //m_mazeSystem.DisplayList(m_characterListOnBottomFloor);
        //m_mazeSystem.DisplayList(m_characterListOnTopFloor);
    }

    private void Awake()
    {
        m_mazeSystem = GameObject.FindObjectOfType<MazeSystem>();
    }
}
