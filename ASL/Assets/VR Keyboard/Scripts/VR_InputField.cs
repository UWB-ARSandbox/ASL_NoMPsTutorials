using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class VR_InputField : MonoBehaviour
{
    InputField inputField;
    Keyboard keyboard;

    // Start is called before the first frame update
    void Start()
    {
        inputField = gameObject.GetComponent<InputField>();
        keyboard = FindObjectOfType<Keyboard>();
    }

    private void Update()
    {
        if (inputField.isFocused)
        {
            keyboard.SetInputField(inputField);
        }
    }
}
