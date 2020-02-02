using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
#region Winner
    [Header("Winner")]
    public Text WinnerText;
    public Totem[] totems;
    public DummyMove[] players;
#endregion

    void Start()
    {
        Timeout.enabled = false;
        WinnerText.enabled = false;
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
            StartCoroutine(BeforeReload());
        }

        foreach(Totem i in totems)
        {
            if(i.thereIsAWinner)
            {
                WinnerText.enabled = true;
                WinnerText.text = players[i.playerTotem].CharacterName.ToString() + " is the winner";
            }
        }
    }

    IEnumerator BeforeReload()
    {
        yield return new WaitForSeconds(5f);
        if (Input.anyKeyDown)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
