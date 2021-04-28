using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEndPosition : MonoBehaviour
{
    private PlayerSystem m_playerSystem;
    public MazeSystem m_mazeSystem;
    public float endLevelDistance = 1f;
    private bool m_isLevelEnded = false;

    // Start is called before the first frame update
    private void Start()
    {
        m_playerSystem = GameObject.FindObjectOfType<PlayerSystem>();
    }

    private void CheckDistance()
    {
        if (!m_playerSystem.GetIsHost()) return;
        if (m_isLevelEnded is true) { return; }

        int numPlayerInLevel = m_mazeSystem.GetNumCharacterInMaze();

        if (numPlayerInLevel <= 0) { return; }

        bool isAnyCharacNotEnded = false;
        for (int i = 0; i < numPlayerInLevel; i++)
        {
            GameObject character = m_mazeSystem.GetMazeCharacterByIndex(i);
            //Debug.Log("Character name: " + character.name); // Test if character is added
            float dist = Vector3.Distance(character.transform.position, this.gameObject.transform.position);
            //Debug.Log("Character : " + character.name + " distance to end position is " + dist);
            if (dist > endLevelDistance)
            {
                Debug.Log("Some character has not pass the level");
                isAnyCharacNotEnded = true;
            }
        }

        if (isAnyCharacNotEnded is false)
        {
            Debug.Log("Level passed");
            m_isLevelEnded = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        CheckDistance();
    }
}
