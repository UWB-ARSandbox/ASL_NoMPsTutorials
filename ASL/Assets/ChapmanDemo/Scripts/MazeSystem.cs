using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeSystem : MonoBehaviour
{
    // Test/Demo user system
    private DemoSystem m_demoSystem;

    private MazeStartPosition m_mazeStartPosition;
    private MazeEndPosition m_mazeEndPosition;
    private List<GameObject> m_characterList = new List<GameObject>();
    private List<GameObject> m_characterListOnBottomFloor = new List<GameObject>();
    private List<GameObject> m_characterListOnTopFloor = new List<GameObject>();
    private bool m_isMazeEnded = false;

    /* Functionality for add and accessing character lists */

    // Add character
    public void AddCharacterInMaze(GameObject character) { m_characterList.Add(character); }
    public void AddBottomFloorCharac(GameObject character) { m_characterListOnBottomFloor.Add(character); }
    public void AddTopFloorCharac(GameObject character) { m_characterListOnTopFloor.Add(character); }

    // Number of character in the list
    public int GetNumCharacterInMaze() { return m_characterList.Count; }
    public int GetNumCharacterInBottomFloor() { return m_characterListOnBottomFloor.Count; }
    public int GetNumCharacterInTopFloor() { return m_characterListOnTopFloor.Count; }

    // Accessing GameObject character in the list
    public GameObject GetMazeCharacterByIndex(int index)
    {
        if (index >= m_characterList.Count) return null;
        return m_characterList[index];
    }
    public GameObject GetBottomFloorCharByIndex(int index)
    {
        if (index >= m_characterListOnBottomFloor.Count) return null;
        return m_characterListOnBottomFloor[index];
    }
    public GameObject GetTopFloorCharByIndex(int index)
    {
        if (index >= m_characterListOnTopFloor.Count) return null;
        return m_characterListOnTopFloor[index];
    }

    // Display List<GameObject>
    public void DisplayList(List<GameObject> list)
    {
        Debug.Log("Num in list = " + list.Count);
        foreach (GameObject obj in list)
        {
            Debug.Log("Obj name = " + obj.name);
        }
    }

    /* Set and Get Function for isMazeEnded */
    public void SetIsMazeEnded(bool isEnded) { m_isMazeEnded = isEnded; }
    public bool GetIsMazeEnded() { return m_isMazeEnded; }

    /* Private initialize functions */

    private void InitMaze()
    {
        // Get players
        var character = m_demoSystem.GetCharacter(); // TODO: Replace with new player/user system

        // Add players into the start position
        AddCharacterInMaze(character); // TODO: Use a loop to add players

        // Start Maze
        m_mazeStartPosition.PlaceCharacterInStartPos();
    }

    // Start is called before the first frame update
    private void Start()
    {
        // Demo system only
        m_demoSystem = GameObject.FindObjectOfType<DemoSystem>();

        m_mazeStartPosition = GameObject.FindObjectOfType<MazeStartPosition>();
        m_mazeEndPosition = GameObject.FindObjectOfType<MazeEndPosition>();
        InitMaze();
    }
}
