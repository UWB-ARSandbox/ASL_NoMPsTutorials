using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;
/*This script parse through all object after 0.05 second to find all ASL objects and apply ASLTransformSync scripts to them */
public class ASLObejctsHandler : MonoBehaviour
{
    public  bool FoundAllASL = false; //True if all object was parse through
    private List<GameObject> ASLObjects = new List<GameObject>();   //Stores all ASL object 
    
    //private List<Vector3> scale = new List<Vector3>();
    // Start is called before the first frame update
    void Start()
    {
        FoundAllASL = false;
        StartCoroutine(findAllASLObject());
    }
    //Apply ASLTransformSync to all ASL object 3 seconds after the game start
    IEnumerator findAllASLObject()
    {
        yield return new WaitForSeconds(3f);
        object[] obj = GameObject.FindObjectsOfType(typeof(GameObject));
        foreach (object o in obj)
        {
            GameObject g = (GameObject)o;
            if (g.GetComponent<ASLObject>())
            {
                ASLObjects.Add(g);
                if (g.tag == "Player")
                    g.AddComponent<ASLPlayerSync>();
                else
                    g.AddComponent<ASLTransformSync>();
            }  
            
        }
        
        FoundAllASL = true;

    }


    
}
