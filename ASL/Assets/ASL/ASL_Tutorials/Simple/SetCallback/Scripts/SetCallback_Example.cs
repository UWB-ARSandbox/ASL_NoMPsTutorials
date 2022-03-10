using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SimpleDemos
{
    /// <summary>Example of how to set a OpFunction callback</summary>
    public class SetCallback_Example : MonoBehaviour
    {
        /// <summary>Pre-defined value for how many cubes to create. </summary>
        private static int m_NumOfCubes = 4;
        /// <summary>Pre-derfined value for a cube to be deleted after this amount of moving time. </summary>
        private static float m_LimitationInSecond = 5;
        /// <summary>Pre-derfined value for movement. </summary>
        private static Vector3 m_AdditiveMovementAmount = new Vector3(0.01f, 0.01f, 0.01f);
        /// <summary>Dictinary for storing cubes and its moving time </summary>
        private static Dictionary<GameObject, float> m_MyCubesAndTimerDictionary = new Dictionary<GameObject, float>();

        /// <summary>Game Logic</summary>

        /// <summary>Initialize cubes with created callback function for adding it to the dictionary</summary>
        void Start()
        {
            for(int i=0; i< m_NumOfCubes; i++)
            {
                ASL.ASLHelper.InstantiateASLObject(PrimitiveType.Cube,
                    new Vector3(Random.Range(-4f, 1f), Random.Range(0f, 0.5f), Random.Range(-2f, 2f)),
                    Quaternion.identity, "", "",
                    CubeCreatedCallback);
            }
        }

        /// <summary>Update cubes' movement in the dictionary
        /// If a cube reaches its moving time limit, it will be deleted by the OpFunction callback,
        /// Otherwise, keep moving.
        /// </summary>
        void Update()
        {
            foreach (GameObject _cube in m_MyCubesAndTimerDictionary.Keys.ToList())
            {
                float timer = m_MyCubesAndTimerDictionary[_cube];
                timer += Time.deltaTime;
                if ((int)timer % 60 > m_LimitationInSecond)
                {
                    _cube.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        _cube.GetComponent<ASL.ASLObject>().SendAndIncrementLocalPosition(m_AdditiveMovementAmount, EndOfMovementCallback);
                    });
                } else
                {
                    _cube.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        _cube.GetComponent<ASL.ASLObject>().SendAndIncrementLocalPosition(m_AdditiveMovementAmount);
                    });
                    m_MyCubesAndTimerDictionary[_cube] = timer;
                }
            }
        }

        /// <summary>InstantiateASLObject function's callback</summary>
        private static void CubeCreatedCallback(GameObject _object)
        {
            m_MyCubesAndTimerDictionary.Add(_object, 0);
        }

        /// <summary>OpFunction's callback</summary>
        private void EndOfMovementCallback(GameObject _object)
        {
            m_MyCubesAndTimerDictionary.Remove(_object);
            _object.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                _object.GetComponent<ASL.ASLObject>().DeleteObject();
            });
        }
    }
}
