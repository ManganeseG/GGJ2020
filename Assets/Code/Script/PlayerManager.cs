using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region public Variables
    public int maxNumberOfPlayers = 4;
    public GameObject[] PlayerController;
    #endregion

    #region _private Variables
    int _indexController = 0;
    int[] _playerControllers;
    PlayerController[] _playersScripts;
    List<int> _usedIndex = new List<int>();

    #endregion

    void Start()
    {
        _playerControllers = new int[maxNumberOfPlayers];
        for (int i = 0; i < _playerControllers.Length; i++)
        {
            _playerControllers[i] = -1;
        }

        _playersScripts = new PlayerController[PlayerController.Length];
        for (int i = 0; i < _playersScripts.Length; i++)
        {
            _playersScripts[i] = PlayerController[i].GetComponent<PlayerController>();
        }
    }

    void Update()
    {
        for (int i = 0; i < 5; i++)
            if (Input.GetButtonDown("Action_" + i) && _indexController < maxNumberOfPlayers)
            {
                for (int j = 0; j < _playerControllers.Length; j++)
                {
                    Debug.Log(_playerControllers[j] + "_" + i);
                    if (_playerControllers[j] != i)
                    {
                        _usedIndex.Add(i);
                        _playerControllers[_indexController] = i;
                        _playersScripts[_indexController].inputIndex = i;
                        PlayerController[_indexController].SetActive(true);
                        _indexController++;
                        break;
                    }
                }
            }

        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < _playerControllers.Length; i++)
            {
                Debug.Log(_playerControllers[i]);
            }
        }
    }

    void SetupPlayers()
    {

    }
}