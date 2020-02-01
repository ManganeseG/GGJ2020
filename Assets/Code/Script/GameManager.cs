using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
#region Timer
    public Text Timer;
    public float TimerValue;
    #endregion

    void Start()
    {
        
    }

    void Update()
    {
        TimerValue -= Time.deltaTime;
        Timer.text = " " + Mathf.Round(TimerValue);


    }
}
