using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateProblem : MonoBehaviour
{
    public float gFrom = 8f, gTo = 10f, m1From = 90f, m1To = 110f, m2From = 70f, m2To = 80f;
    public double h1 = 20.5, h2 = 10.5, l = 0.5;
    public GameObject gText, m1Text, m2Text, resultText;
    private double g, m1, m2;
    // Start is called before the first frame update
    void Start()
    {
        g = Math.Round(UnityEngine.Random.Range(gFrom, gTo), 2);
        m1 = Math.Round(UnityEngine.Random.Range(m1From, m1To), 2);
        m2 = Math.Round(UnityEngine.Random.Range(m2From, m2To), 2);
        
        double v2 = Math.Sqrt(2*g*(h2 - l));
        double v1 = ((m1 + m2)/m1)*v2;
        double v0 = Math.Sqrt(v1*v1 - 2*g*(h1 - l));
        
        gText.GetComponent<TextMeshProUGUI>().text = "g = " + g;
        m1Text.GetComponent<TextMeshProUGUI>().text = "m1 = " + m1;
        m2Text.GetComponent<TextMeshProUGUI>().text = "m2 = " + m2;
        resultText.GetComponent<TextMeshProUGUI>().text = "Resultado = " + v0;
        
        Debug.Log("Result is " + v0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}