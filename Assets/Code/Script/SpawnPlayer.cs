using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Transform[] PlayersSpawnPos;
    public GameObject[] playerCharacter;


    //public GameObject Cat;
    //public GameObject Axolotl;
    //public GameObject Owl;
    //public GameObject Goat;

    //private GameObject selectedCharacter;


    void Start()
    {
        for(int i = 0; i <= playerCharacter.Length; i++)
        {
            playerCharacter[i].transform.position = PlayersSpawnPos[i].position;
        }

    }
    


    void Update()
    {
        
    }
}
