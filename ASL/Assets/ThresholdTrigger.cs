using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ThresholdTrigger : MonoBehaviour
{
    public int Threshold;
    public UnityEvent OnTriggered;
    public UnityEvent OnUntriggered;
    public UnityEvent<int> OnChange;


    private int activations;

    // Start is called before the first frame update
    void Start()
    {
        activations = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void HitStart()
    {
        activations++;
        if (activations == Threshold)
        {
            OnTriggered.Invoke();
        }
        OnChange.Invoke(activations);
    }

    public void HitEnd()
    {
        activations--;
        if (activations == Threshold - 1)
        {
            OnUntriggered.Invoke();
        }
        OnChange.Invoke(activations);
    }
}
