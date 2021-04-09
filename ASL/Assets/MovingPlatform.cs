using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public MovingPlatformPoint From;
    public MovingPlatformPoint To;
    public float Speed;
    public float RelativePosition;
    private float fromTime;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (From == null || To == null || From.Delay > Time.time - fromTime)
        {
            return;
        }

        if (Speed == 0)
        {
            // Allow From to delay for however long by setting speed to zero
            if (From.SetSpeed)
            {
                Speed = From.Speed;
            }
            if (Speed == 0)
            {
                return;
            }
        }

        float distance = Speed * Time.deltaTime;
        Vector3 v = To.transform.position - From.transform.position;
        float NextRelativePosition = RelativePosition + distance / v.magnitude;

        // Check if next position is at or past the destination
        if (NextRelativePosition >= 1f)
        {
            // Update From and To
            From = To;
            if (From == null)
            {
                To = null;
                Speed = 0;
                RelativePosition = 0;
                return;
            }
            To = From.Next;

            // Update Speed
            if (From.SetSpeed)
            {
                Speed = From.Speed;
            }

            v = To.transform.position - From.transform.position;

            fromTime = Time.time;

            if (From.Delay <= 0)
            {
                distance -= (NextRelativePosition - 1) * v.magnitude;
                NextRelativePosition = distance / v.magnitude;
            } else
            {
                NextRelativePosition = 0;
            }
        }

        RelativePosition = NextRelativePosition;

        transform.position = From.transform.position + v * RelativePosition;
    }
}
