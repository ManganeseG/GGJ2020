using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    #region public Variables
    public int maxNumberOfPlayers = 4;
    public GameObject[] playersObjects; //May be deleted
    public PlayerSelector[] selectorScripts;
    #endregion

    #region _private Variables
    [HideInInspector] public int numberOfPlayers = 0;
    int[] _playerControllers;
    PlayerController[] _playersScripts;
    int _playerIndex;
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
                for (int j = 0; j < maxNumberOfPlayers; j++)
                {
                    if (_playerControllers[j] == -1)
                    {
                        _playerIndex = j;
                        break;
                    }
                }
                //Then Apply it to him
                _playerControllers[_playerIndex] = i;
                _playersScripts[_playerIndex].inputIndex = i;
                selectorScripts[_playerIndex].inputIndex = i;
                StartCoroutine(selectorScripts[_playerIndex].addPlayer(_playerIndex));
                numberOfPlayers++;
                break;
            }
    }

    public void RemovePlayer(int _playerIndex, int _controllerIndex)
    {
        _playerControllers[_playerIndex] = -1;
        _playersScripts[_playerIndex].inputIndex = -1;
        selectorScripts[_playerIndex].inputIndex = -1;
        numberOfPlayers--;
    }

    public bool canLockChara(int playerIndex, int charaIndex)
    {
        for (int i = 0; i < selectorScripts.Length; i++)
        {
            if (i != playerIndex && selectorScripts[i].currentIndex == charaIndex && selectorScripts[i]._isReady)
            {
                return false;
            }
        }
        return true;
    }
}