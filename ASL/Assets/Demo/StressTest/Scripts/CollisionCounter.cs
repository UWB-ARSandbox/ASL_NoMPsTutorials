using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour
{
    public Text Counter;
    int numberOfCollision = 0;

    public void IncrementCount()
    {
        numberOfCollision++;
        Counter.text = numberOfCollision.ToString();
    }
}
