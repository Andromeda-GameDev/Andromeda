using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LvlOneInput : MonoBehaviour
{
    public TextMeshProUGUI pedalSpeedInputText;
    public GyroscopeData gsData;
    public LvlOneDataGenerator dataGenerator;
    // public QuestionManager qManager;
    public HoldButton pedalHoldButton;
    public GameObject pedalButton, redoButton, submitButton, canvas;
    private bool pedalPressed;
    float answer, pedalSpeedInput, startingYAttitude, startingZAttitude, greatestAnswer = 20;
    private int stage = 0;
    bool firstTouch = true, answered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Check for the value corresponding to the stage
    void OnEnable()
    {
        answered = false;
        // get stage
        // stage = qManager.stage;
        switch(stage)
        {
            case 0:
                answer = (float)dataGenerator.v2;
                break;
            case 1:
                answer = (float)dataGenerator.v1;
                break;
            case 2:
                answer = (float)dataGenerator.v0;
                break;
            default:
                answer = 0;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if touched
        float n = 0, pow = 0, div = 0;
        if (pedalHoldButton.holding) {
            if (firstTouch) {
                pedalSpeedInput = 0.0f;
                startingYAttitude = gsData.attitude.y;
                startingZAttitude = gsData.attitude.z;
                firstTouch = false;
            }
            else
            {
                float randCenter = (float)answer + UnityEngine.Random.Range(-0.5f, 0.5f);
                n = ((startingYAttitude - gsData.attitude.y) / 4);
                div = (float)(n / randCenter);
                pow = (float)Math.Pow((double)(div - 1), 3.0);
                pedalSpeedInput = (pow + 1) * randCenter;

                // Math.Pow((double)((((startingYAttitude - gsData.attitude.y) / 4) / dataGenerator.maxV0) - 1), 3.0)

                // speed += Mathf.Abs(acceleration.z / 1.5f);
                // speed += 1.0f;
            }
            if(pedalSpeedInput < 0)
            {
                pedalSpeedInputText.color = Color.red;
            }
            else if(pedalSpeedInput > answer*2)
            {
                pedalSpeedInputText.color = Color.red;
            }
            else
            {
                pedalSpeedInputText.color = Color.LerpUnclamped(Color.blue, Color.red, pedalSpeedInput/(answer*2));
            }
            pedalSpeedInputText.text = $"{pedalSpeedInput:#0.00}";
        }
        else
        {
            // qué es más rápido, asignación o evaluación de un valor booleano?
            if(!firstTouch)
            {
                pedalSpeedInputText.color = Color.yellow;
                pedalButton.SetActive(false);
                redoButton.SetActive(true);
                submitButton.SetActive(true);
                firstTouch = true;
            }
        }
    }

    public void Redo()
    {
        pedalButton.SetActive(true);
        redoButton.SetActive(false);
        submitButton.SetActive(false);
    }

    public void CheckResults()
    {
        /*
            0 -> correct
            1 -> less than
            2 -> more than
        */
        int userAnswer;
        if(pedalSpeedInput > (answer - 0.1f) && pedalSpeedInput < (answer + 0.1f))
        {
            // Correct
            userAnswer = 0;
            pedalSpeedInputText.color = Color.green;
        }
        else if(pedalSpeedInput <= (answer - 0.1f))
        {
            // Incorrect (less than)
            userAnswer = 1;
        }
        else
        {
            userAnswer = 2;
        }
        // Send values before kms
        qManager.userAnswer = userAnswer;
        answered = true;
        // kms
        canvas.SetActive(false);
    }

    public void Leave()
    {
        canvas.SetActive(false);
    }
}
