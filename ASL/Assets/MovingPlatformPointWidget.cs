using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovingPlatformPointWidget : MonoBehaviour
{
    public MovingPlatformPoint Point;
    public TMP_InputField Speed;
    public TMP_InputField Delay;
    public Toggle SetSpeed;
    public Dropdown Next;
    public List<MovingPlatformPoint> AvailablePoints;
    // Start is called before the first frame update
    void Start()
    {
        Speed.onValueChanged.AddListener((string s) =>
        {
            Point.Speed = float.Parse(s);
        });
        Delay.onValueChanged.AddListener((string s) =>
        {
            Point.Delay = float.Parse(s);
        });
        SetSpeed.onValueChanged.AddListener((bool b) =>
        {
            Point.SetSpeed = b;
        });
        /*
        Next.onValueChanged.AddListener((int i) => {
            Point.Next = AvailablePoints[i];
        });*/
    }

    // Update is called once per frame
    void Update()
    {
        Speed.text = "" + Point.Speed;
        Delay.text = "" + Point.Delay;
        SetSpeed.isOn = Point.SetSpeed;
        //Next.value = 'A' + AvailablePoints.IndexOf(Point.Next);
    }
}
