using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ASL;

public class ButtonPress : MonoBehaviour
{
    public GameObject keyOne;
    public GameObject keyTwo;
    public GameObject door;
    public GameObject track1;
    public GameObject track2;
    public GameObject track3;
    bool button1 = false;
    bool button2 = false;
    public float[] m_MyFloats = new float[4];
    public bool m_SendFloat = false;
    // Start is called before the first frame update
    void Start()
    {
        keyOne.GetComponent<ASL.ASLObject>()._LocallySetFloatCallback(MyFloatFunction);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_SendFloat)
        {
            keyOne.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
            {
                string floats = "Floats sent: ";
                for (int i = 0; i < m_MyFloats.Length; i++)
                {
                    floats += m_MyFloats[i].ToString();
                    if (m_MyFloats.Length - 1 != i)
                    {
                        floats += ", ";
                    }
                }
                Debug.Log(floats);
                keyOne.GetComponent<ASL.ASLObject>().SendFloatArray(m_MyFloats);
            });
            m_SendFloat = false;
        }

        while (keyOne.GetComponent<Renderer>().material.color == Color.red && keyTwo.GetComponent<Renderer>().material.color == Color.green && door.transform.position.y <= 2.6)
        {
            door.transform.position += new Vector3(0, 5, 0);
        }

        while ((keyOne.GetComponent<Renderer>().material.color == Color.white || keyTwo.GetComponent<Renderer>().material.color == Color.white) && door.transform.position.y > 2.5)
        {
            door.transform.position += new Vector3(0, -5, 0);
        }

        if (Input.GetMouseButtonDown(0))
        {
            int layerMask = 1 << 10;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100, layerMask))
            {
                if(hit.transform.name == "Button")
                {
                    
                    if (!button1)
                    {
                        keyOne.GetComponent<Renderer>().material.color = Color.red;
                        keyOne.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                        {
                            keyOne.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(Color.red, Color.red);
                        });
                        button1 = true;
                        m_MyFloats[0] = 0;
                    }
                    else
                    {
                        keyOne.GetComponent<Renderer>().material.color = Color.white;
                        keyOne.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                        {
                            keyOne.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(Color.white, Color.white);
                        });
                        button1 = false;
                        m_MyFloats[0] = 1;
                    }
                    keyOne.GetComponent<ASL.ASLObject>().SendFloatArray(m_MyFloats);
                    //m_SendFloat = true;
                }

                if (hit.transform.name == "Button2")
                {
                    if (button1 && !button2)
                    {
                        keyTwo.GetComponent<Renderer>().material.color = Color.green;
                        keyTwo.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                        {
                            keyTwo.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(Color.green, Color.green);
                        });
                        button2 = true;
                    }
                    else
                    {
                        keyTwo.GetComponent<Renderer>().material.color = Color.white;
                        keyTwo.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                        {
                            keyTwo.GetComponent<ASL.ASLObject>().SendAndSetObjectColor(Color.white, Color.white);
                        });
                        button2 = false;
                    }

                }

                //track demo
                if (hit.transform.name == "Button3")
                {
                    track1.transform.Rotate(0, 90, 0);                 
                    track1.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        track1.GetComponent<ASL.ASLObject>().SendAndSetWorldRotation(track1.transform.rotation);
                    });

                }

                if (hit.transform.name == "Button4")
                {
                    track2.transform.Rotate(0, 90, 0);
                    track2.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        track2.GetComponent<ASL.ASLObject>().SendAndSetWorldRotation(track2.transform.rotation);
                    });

                }

                if (hit.transform.name == "Button5")
                {
                    track3.transform.Rotate(0, 90, 0);
                    track3.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        track3.GetComponent<ASL.ASLObject>().SendAndSetWorldRotation(track3.transform.rotation);
                    });
                }
            }

        }

        
    }

    public static void MyFloatFunction(string _id, float[] _myFloats)
    {
        string floats = "Floats received: ";
        for (int i = 0; i < _myFloats.Length; i++)
        {
            floats += _myFloats[i].ToString();
            if (_myFloats.Length - 1 != i)
            {
                floats += ", ";
            }
        }
        Debug.Log(floats);
        //The following is a hypothetical switch that a user could create. While we are sending values in this example
        //We aren't actually sending what the comments in the case statement are saying (e.g., we aren't sending a player's
        //score.) We are simply giving an example of what could be sent using this switch method. If the user only has
        //a need for 4 or less values, then obviously a switch statement is not necessary.
        switch (_myFloats[3])
        {
            case 0:
                Debug.Log("The values sent were: " + _myFloats[0] + ", " + _myFloats[1]
                                             + ", " + _myFloats[2] + ", " + _myFloats[3]);
                break;
            case 1: //Sent Player's health
                Debug.Log("Using case: " + _myFloats[3]);
                Debug.Log("The Player's health is: " + _myFloats[0]);
                break;
            case 2: //Sent Player's score
                Debug.Log("Using case: " + _myFloats[3]);
                Debug.Log("The Player's score is: " + _myFloats[0]);
                break;
            case 3: //Sent Player's velocity and direction
                Debug.Log("Using case: " + _myFloats[3]);
                Debug.Log("The Player's velocity is: " + _myFloats[0]);
                Debug.Log("The Player's direction is: " + _myFloats[1]);
                break;
            case 4: //Sent random values
                Debug.Log("Using case: " + _myFloats[3]);
                Debug.Log("Random value 1: " + _myFloats[0]);
                Debug.Log("Random value 2: " + _myFloats[1]);
                Debug.Log("Random value 3: " + _myFloats[2]);
                break;
            case 5: //Sent random values
                if (ASL.ASLHelper.m_ASLObjects.TryGetValue(_id, out ASL.ASLObject myObject))
                {
                    Debug.Log("The name of the object that sent these floats is: " + myObject.name);
                }
                break;
            default:
                Debug.Log("This example does not do anything specific for m_MyFloats[3] above 5");
                break;
        }
    }
}
