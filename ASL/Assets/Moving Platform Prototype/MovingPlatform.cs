using ASL;
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
    private bool delayed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (From != null && To == null)
        {
            To = From.Next;

        }

        if (From == null || To == null || (delayed && From.Delay > Time.time - fromTime))
        {
            return;
        }
        delayed = false;

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
        float NextRelativePosition;
        if (v.magnitude != 0)
        {
            NextRelativePosition = RelativePosition + distance / v.magnitude;
        } else
        {
            NextRelativePosition = RelativePosition;
        }

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
            if (To == null)
            {
                Speed = 0;
                RelativePosition = 0;
                return;
            }

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
                if (v.magnitude != 0)
                {
                    NextRelativePosition = distance / v.magnitude;
                }
                else
                {
                    NextRelativePosition = 0;
                }
            } else
            {
                delayed = true;
                NextRelativePosition = 0;
            }
        }

        RelativePosition = NextRelativePosition;

        Debug.Log("USER ID = " + ASLUserID.ID());
        if (ASLUserID.ID() == 0)
        {
            GetComponent<ASLObject>().SendAndSetClaim(() => {
                Vector3 newPos = From.transform.position + v * RelativePosition;
                if (newPos.x != newPos.x)
                {
                    Debug.Log("NaN in newPos.x! newPos = " + newPos + " from pos: " + From.transform.position + " v: " + v + " RelativePosition: " + RelativePosition);
                }
                GetComponent<ASLObject>().SendAndSetWorldPosition(newPos);
            });
        }
    }
}