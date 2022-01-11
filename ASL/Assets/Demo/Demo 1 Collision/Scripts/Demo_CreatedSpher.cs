using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demo_CreatedSpher : MonoBehaviour
{
    [Tooltip("How long this object will exist before destroying iteself.")]
    public float LifeSpan = 5.0f;

    float timer = 0.0f;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= LifeSpan)
        {
            Destroy(gameObject);
        }
    }
}
