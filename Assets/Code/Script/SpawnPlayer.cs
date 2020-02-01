using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    public Transform[] PlayersSpawnPos;

    public GameObject Cube;
    public GameObject Sphere;
    public GameObject Capsule;
    public GameObject Cylinder;

    private GameObject selectedCharacter;


    void Start()
    {
        for(int i = 0; i < PlayersSpawnPos.Length; i++)
        {
            Instantiate(Sphere, PlayersSpawnPos[i]); //replace sphere by selected char
        }   
    }
    


    void Update()
    {
        
    }
}
