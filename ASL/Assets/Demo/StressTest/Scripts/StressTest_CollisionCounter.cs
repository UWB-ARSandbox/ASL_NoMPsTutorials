using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ASL;
using System.IO;

public class StressTest_CollisionCounter : MonoBehaviour
{
    public MovingObject[] MovingObjects;
    public Text DisplayText;

    float collisionCount = 0;

    string fileName = "";

    // Start is called before the first frame update
    void Start()
    {
        //Setting ASL float function
        gameObject.GetComponent<ASL.ASLObject>()._LocallySetFloatCallback(updateCounter);
        fileName += ASL.GameLiftManager.GetInstance().m_Username + ".txt";

        using (StreamWriter sw = File.CreateText(fileName))
        {
            string outputTxt = "Output Log for: " + ASL.GameLiftManager.GetInstance().m_Username + "\n\n";
            sw.WriteLine(outputTxt);
        }

    }

    void updateCounter(string _id, float[] count)
    {
        Debug.Log(collisionCount);
        collisionCount++;
        if (collisionCount % 10 == 0)
        {
            string outputTxt = "Collision Count: " + collisionCount + "\n";
            foreach (MovingObject movingObject in MovingObjects)
            {
                outputTxt += movingObject.name + ": " + movingObject.transform.position + "\n";
            }
            DisplayText.text = outputTxt;
            outputTxt += "\n=================================\n";
            using (StreamWriter sw = File.AppendText(fileName))
            {
                sw.WriteLine(outputTxt);
            }
            Debug.Log(outputTxt);
        }
    }
}
