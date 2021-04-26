﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeStartPosition : MonoBehaviour
{
    public SpawnArea topFloorSpawnArea;
    public SpawnArea bottomFloorSpawnArea;
    private PlayerSystem m_playerSystem;
    public MazeSystem m_mazeSystem;
    [SerializeField] private float m_pos_y = 1f;
    [SerializeField] private float m_secFloorPos_y = 12f;

    public void PlaceCharacterInStartPos()
    {
        if (!m_playerSystem.GetIsHost()) return;

        //Debug.Log("Position = " + pos);
        int numCharacterInMaze = m_mazeSystem.GetNumCharacterInMaze();

        if (numCharacterInMaze <= 0)
        {
            Debug.Log("Error: No character added to the maze");
            return;
        }
        //DisplayObjectPosition();
        for (int i = 0; i < numCharacterInMaze; i++)
        {
            GameObject character = m_mazeSystem.GetMazeCharacterByIndex(i);
            string id = character.GetComponent<ASL.ASLObject>().m_Id;
            //Debug.Log("character id = " + id);
            Debug.Log("i = " + i + "i % 2 = " + (i % 2));
            if (i % 2 == 0) // even number
            {
                Debug.Log("Bottom floor");
                Vector3 bottomFloorPos = bottomFloorSpawnArea.GetEmptySpawnPosition();
                // Set character position to bottom floor
                character.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                {
                    character.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(bottomFloorPos);
                });
                //Debug.Log("Add Character name: " + character.name + " to bottom floor list");
                m_mazeSystem.AddBottomFloorCharac(character);
            }          
            else
            {
                Debug.Log("Top floor");
                Vector3 topFloorPos = topFloorSpawnArea.GetEmptySpawnPosition();
                character.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                {
                    character.GetComponent<ASL.ASLObject>().SendAndSetWorldPosition(topFloorPos);
                });
                m_mazeSystem.AddTopFloorCharac(character);
            }
            
        }
        //DisplayObjectPosition();
        //m_mazeSystem.DisplayList(m_characterListOnBottomFloor);
        //m_mazeSystem.DisplayList(m_characterListOnTopFloor);
    }

    private void DisplayObjectPosition()
    {
        int numCharacterInMaze = m_mazeSystem.GetNumCharacterInMaze();
        Debug.Log("================================");
        for (int i = 0; i < numCharacterInMaze; i++)
        {
            GameObject character = m_mazeSystem.GetMazeCharacterByIndex(i);
            string id = character.GetComponent<ASL.ASLObject>().m_Id;
            Debug.Log("Character: " + character.name + " with id " + id +" position is " + character.transform.localPosition);
        }
        Debug.Log("================================");
    }

    private void Awake()
    {
        m_playerSystem = GameObject.FindObjectOfType<PlayerSystem>();
        //m_mazeSystem = GameObject.FindObjectOfType<MazeSystem>();
    }

    private void Update()
    {
        //DisplayObjectPosition();
    }
}
