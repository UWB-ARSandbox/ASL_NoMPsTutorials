using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingUIElement : MonoBehaviour
{
    public float DisplayDuration;
    public float FadeDuration;
    private float alpha;
    private float displayTimer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (displayTimer > 0)
        {
            displayTimer -= Time.deltaTime;
        }
        else
        {
            alpha = Mathf.Max(0, alpha - Time.deltaTime / FadeDuration);
            GetComponent<CanvasRenderer>().SetAlpha(alpha);
        }
    }

    public void Trigger()
    {
        alpha = 1;
        GetComponent<CanvasRenderer>().SetAlpha(alpha);
        displayTimer = DisplayDuration;
    }
}
