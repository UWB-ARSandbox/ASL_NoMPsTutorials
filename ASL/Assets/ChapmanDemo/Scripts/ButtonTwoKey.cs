using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTwoKey : MonoBehaviour
{
    public GameObject keyOne;
    public GameObject keyTwo;
    public Color keyColor;
    public Color keyTwoColor;
    public Color buttonColor;
    bool button1 = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 10;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100, layerMask))
            {
                if(hit.transform.GetComponent<Renderer>().material.color == buttonColor)
                {
                    if (keyOne.GetComponent<Renderer>().material.color == keyColor)
                    {
                        if (!button1)
                        {
                            keyTwo.GetComponent<Renderer>().material.color = keyTwoColor;
                            keyTwo.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                            {
                                keyTwo.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(keyTwoColor, keyTwoColor);
                            });
                            button1 = true;
                        }
                        else
                        {
                            keyTwo.GetComponent<Renderer>().material.color = Color.white;
                            keyTwo.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                            {
                                keyTwo.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(Color.white, Color.white);
                            });
                            button1 = false;
                        }
                    }
                }
                               
            }
        }
    }
}
