using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoObject : MonoBehaviour
{
    [Tooltip("The default color of the object. NOTE: The selected color is yellow, the default " +
        "color should be something else.")]
    public Color DefaultColor;
    
    Color selectedColor = Color.yellow;
    const float MOVEMENT_SPEED = 1.0f;
    bool isSelected = false;
    public bool IsSelected
    {
        get { return isSelected; }
    }
    Material material;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(GetComponent<MeshRenderer>() != null);
        material = GetComponent<MeshRenderer>().material;
        material.color = DefaultColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSelected)
        {
            if (Input.GetKey(KeyCode.UpArrow) ^ Input.GetKey(KeyCode.DownArrow))
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        Vector3 m_AdditiveMovementAmount = Vector3.up * MOVEMENT_SPEED * Time.deltaTime;
                        gameObject.GetComponent<ASL.ASLObject>().SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                    });
                }
                else
                {
                    gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        Vector3 m_AdditiveMovementAmount = Vector3.down * MOVEMENT_SPEED * Time.deltaTime;
                        gameObject.GetComponent<ASL.ASLObject>().SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                    });
                }
            }
            if (Input.GetKey(KeyCode.RightArrow) ^ Input.GetKey(KeyCode.LeftArrow))
            {
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        Vector3 m_AdditiveMovementAmount = Vector3.right * MOVEMENT_SPEED * Time.deltaTime;
                        gameObject.GetComponent<ASL.ASLObject>().SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                    });
                }
                else
                {
                    gameObject.GetComponent<ASL.ASLObject>().SendAndSetClaim(() =>
                    {
                        Vector3 m_AdditiveMovementAmount = Vector3.left * MOVEMENT_SPEED * Time.deltaTime;
                        gameObject.GetComponent<ASL.ASLObject>().SendAndIncrementWorldPosition(m_AdditiveMovementAmount);
                    });
                }
            }
        }
    }

    public void SelectObject(bool _isSelected)
    {
        isSelected = _isSelected;
    }
}
