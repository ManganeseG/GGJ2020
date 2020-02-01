using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    #region public Variables
    public int maxNumberOfPlayers = 4;
    public GameObject[] playersObjects;
    public PlayerSelector[] selectorScripts;
    #endregion

    #region _private Variables
    [HideInInspector] public int numberOfPlayers = 0;
    int[] _playerControllers;
    PlayerController[] _playersScripts;
    #endregion

    void Start()
    {
        _playerControllers = new int[maxNumberOfPlayers];
        for (int i = 0; i < _playerControllers.Length; i++)
        {
            _playerControllers[i] = -1;
        }

        _playersScripts = new PlayerController[playersObjects.Length];
        for (int i = 0; i < _playersScripts.Length; i++)
        {
            _playersScripts[i] = playersObjects[i].GetComponent<PlayerController>();
        }

    }

    void Update()
    {
        //Get whatever controllers is used
        for (int i = 0; i < 5; i++)
            if (Input.GetButtonDown("Action_" + i) && numberOfPlayers < maxNumberOfPlayers)
            {
                //Check if a player already has it registered
                for (int j = 0; j < _playerControllers.Length; j++)
                {
                    if (_playerControllers[j] != i)
                        continue;
                    else
                        return;
                }
                //Then Apply it to him
                _playerControllers[numberOfPlayers] = i;
                _playersScripts[numberOfPlayers].inputIndex = i;
                selectorScripts[numberOfPlayers].inputIndex = i;
                playersObjects[numberOfPlayers].SetActive(true);
                numberOfPlayers++;
                numberOfPlayers++;
                break;
            }
    }

    void SetupPlayers()
    {

    }
}