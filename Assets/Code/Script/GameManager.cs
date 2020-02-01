﻿using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
#region Timer
    [Header("Timer")]
    public Text Timer;
    public float TimerValue;
    [Header("TimeOut Text")]
    public Text Timeout;
    public float LerpSpeed = .5f;
    private float lerpVal = 0f;
    public AnimationCurve timeOutAC;
    #endregion

    void Start()
    {
        Timeout.enabled = false;
    }

    void Update()
    {
        TimerValue -= Time.deltaTime;
        Timer.text = " " + Mathf.Round(TimerValue);
        
        if (TimerValue <= 0)
        {
            TimerValue = 0f;
            Timeout.enabled = true;

            lerpVal += Time.deltaTime * LerpSpeed;
            float timeOutEval = timeOutAC.Evaluate(lerpVal);
            Timeout.rectTransform.localScale = new Vector3(timeOutEval, timeOutEval, timeOutEval);
        }

    }
}
