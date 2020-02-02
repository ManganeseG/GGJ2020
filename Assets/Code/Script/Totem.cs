using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Totem : MonoBehaviour
{
    [Range(0, 3)]
    public int playerTotem;
    public Slider constructionSlider;
    private int winner;
    public bool thereIsAWinner = false;

    void Start()
    {
        constructionSlider.value = 0;
    }
    

    void Update()
    {
        if(constructionSlider.value == 1f)
        {
            thereIsAWinner = true;
        }
    }
}
