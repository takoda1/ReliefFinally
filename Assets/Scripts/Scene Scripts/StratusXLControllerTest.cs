using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StratusXLControllerTest : MonoBehaviour {

    // Use this for initialization
    public float AnalogX = 5.0f;
    public float AnalogY = 5.0f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetButtonDown("Button 0"))
        {
            
            Debug.unityLogger.Log("Button 0 pressed");
        }
        if (Input.GetButtonDown("Button 1"))
        {
            Debug.unityLogger.Log("Button 1 pressed");
        }
        if (Input.GetButtonDown("Button 2"))
        {
            Debug.unityLogger.Log("Button 2 pressed");
        }
        if (Input.GetButtonDown("Button 3"))
        {
            Debug.unityLogger.Log("Button 3 pressed");
        }
        if (Input.GetButtonDown("Button 4"))
        {
            Debug.unityLogger.Log("Button 4 pressed");
        }
        if (Input.GetButtonDown("Button 5"))
        {
            Debug.unityLogger.Log("Button 5 pressed");
        }
        if (Input.GetButtonDown("Button 6"))
        {
            Debug.unityLogger.Log("Button 6 pressed");
        }
        if (Input.GetButtonDown("Button 7"))
        {
            Debug.unityLogger.Log("Button 7 pressed");
        }
        if (Input.GetButtonDown("Button 8"))
        {
            Debug.unityLogger.Log("Button 8 pressed");
        }
        if (Input.GetButtonDown("Button 9"))
        {
            Debug.unityLogger.Log("Button 9 pressed");
        }

        float AX1 = AnalogX * Input.GetAxis("Left Analog X");
        float AY1 = AnalogY * Input.GetAxis("Left Analog Y");
        float AX2 = AnalogX * Input.GetAxis("Right Analog X");
        float AY2 = AnalogY * Input.GetAxis("Right Analog Y");
        float AX3 = AnalogX * Input.GetAxis("DPad X");
        float AY3 = AnalogY * Input.GetAxis("DPad Y");
        float AX4 = AnalogX * Input.GetAxis("L2");
        float AY4 = AnalogY * Input.GetAxis("R2");



        transform.Rotate(AX1, AY1, 0);
        transform.Rotate(AX2, AY2, 0);
        transform.Rotate(AX3, AY3, 0);
        transform.Rotate(AX4, AY4, 0);

    }

}
