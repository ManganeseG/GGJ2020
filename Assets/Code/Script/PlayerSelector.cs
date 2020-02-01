using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSelector : MonoBehaviour
{
    #region public Variables
    public GameObject[] charaObjects;
    public Vector2 selectionTreshold = new Vector2(-.5f, 0.5f);
    public float timeBetweenInputs = 0.1f;

    [HideInInspector] public int inputIndex = -1;
    #endregion

    #region _private Variables
    GameObject[] _charaInstantiated;
    int _currentIndex;
    float _xAxis;
    float _lastInput;
    bool _waitingInput;
    #endregion

    void Start()
    {
        inputIndex = -1;
        _charaInstantiated = new GameObject[charaObjects.Length];
        for (int i = 0; i < _charaInstantiated.Length; i++)
        {
            _charaInstantiated[i] = Instantiate(charaObjects[i], transform);
            _charaInstantiated[i].transform.localPosition = Vector3.zero;
            if (i > 0)
                _charaInstantiated[i].SetActive(false);
        }
    }

    void Update()
    {
        if (inputIndex > -1)
            StartCoroutine(updateAxis(timeBetweenInputs));

        if (_xAxis < selectionTreshold.x)
        {
            _charaInstantiated[_currentIndex].SetActive(false);
            _currentIndex = _currentIndex <= 0 ? _charaInstantiated.Length - 1 : _currentIndex - 1;
            _charaInstantiated[_currentIndex].SetActive(true);
        }

        if (_xAxis > selectionTreshold.y)
        {
            _charaInstantiated[_currentIndex].SetActive(false);
            _currentIndex = _currentIndex >= _charaInstantiated.Length - 1 ? 0 : _currentIndex + 1;
            _charaInstantiated[_currentIndex].SetActive(true);
        }
    }

    IEnumerator updateAxis(float time)
    {
        while (_waitingInput)
        {
            _xAxis = 0;
            Debug.Log("wait_" + _lastInput);
            yield return new WaitForSeconds(time);
            _waitingInput = false;
        }
        while (!_waitingInput)
        {
            Debug.Log("get_" + _lastInput);
            _xAxis = Input.GetAxisRaw("Horizontal_" + inputIndex);
            if (_xAxis != _lastInput)
            {
                _lastInput = _xAxis;
            }
                _waitingInput = true;
        }
        yield return null;
    }
}