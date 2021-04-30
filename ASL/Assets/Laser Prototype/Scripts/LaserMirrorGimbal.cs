using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMirrorGimbal : MonoBehaviour
{

    public float Pitch
    {
        get
        {
            return Mirror.transform.localEulerAngles.x;
        }
        set
        {
            Vector3 x = Mirror.transform.localEulerAngles;
            x.x = value;
            Mirror.transform.localEulerAngles = x;
        }
    }
    public float Yaw
    {
        get
        {
            return Stand.transform.localEulerAngles.y;
        } set
        {
            Vector3 x = Stand.transform.localEulerAngles;
            x.y = value;
            Stand.transform.localEulerAngles = x;
        }
    }
    public float ConstantRotationRatePitch;
    public float ConstantRotationRateYaw;
    public GameObject Stand;
    public GameObject Mirror;
    public float mouseSensitivity;
    public bool mouseRotation;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (ConstantRotationRatePitch != 0f)
        {
            Pitch = Time.time * ConstantRotationRatePitch % 360f;
        }
        if (ConstantRotationRateYaw != 0f) {
            Yaw = Time.time * ConstantRotationRateYaw % 360f;
        }
        //Mirror.transform.localEulerAngles = new Vector3(Pitch, 0f, 0f);
        //Stand.transform.localEulerAngles = new Vector3(0f, Yaw, 0f);
        */
        if (mouseRotation)
        {
            doMouseRotation();
        }
    }

    private void doMouseRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        Vector3 r = Mirror.transform.localEulerAngles;
        r.x += mouseY;
        Mirror.transform.localEulerAngles = r;
        r = Stand.transform.localEulerAngles;
        r.y -= mouseX;
        Stand.transform.localEulerAngles = r;
    }

    public void ToggleMouseRotation()
    {
        mouseRotation = !mouseRotation;
        MouseFirstPersonView mfpv = Camera.main.GetComponent<MouseFirstPersonView>();
        if (mfpv != null)
        {
            mfpv.enabled = !mouseRotation;
        }
    }

    public void SetMouseRotation(bool rotationEnabled)
    {
        mouseRotation = rotationEnabled;
        MouseFirstPersonView mfpv = Camera.main.GetComponent<MouseFirstPersonView>();
        if (mfpv != null)
        {
            mfpv.enabled = !rotationEnabled;
        }
    }
}
