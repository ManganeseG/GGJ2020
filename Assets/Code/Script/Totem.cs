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
    public GameObject totemOwl;
    public GameObject totemGoat;
    public GameObject totemAxolotl;
    public GameObject totemCat;
    private GameObject totemselected;
    public DummyMove playerDummy;

    void Start()
    {
        constructionSlider.value = 0;
        switch (playerDummy.CharacterName)
        {
            case DummyMove.CharaName.Cat:
                totemselected = Instantiate(totemCat, gameObject.transform.position, Quaternion.Euler(new Vector3(-90f, 0f,180f)));
                totemselected.transform.parent = gameObject.transform;
                break;
            case DummyMove.CharaName.Goat:
                totemselected = Instantiate(totemGoat, gameObject.transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 180f)));
                totemselected.transform.parent = gameObject.transform;
                break;
            case DummyMove.CharaName.Axolotl:
                totemselected = Instantiate(totemAxolotl, gameObject.transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 180f)));
                totemselected.transform.parent = gameObject.transform;
                break;
            case DummyMove.CharaName.Owl:
                totemselected = Instantiate(totemOwl, gameObject.transform.position, Quaternion.Euler(new Vector3(-90f, 0f, 180f)));
                totemselected.transform.parent = gameObject.transform;
                break;
            default:
                break;
        }
    }
    

    void Update()
    {
        if(constructionSlider.value == 1f)
        {
            thereIsAWinner = true;
        }
    }
}
