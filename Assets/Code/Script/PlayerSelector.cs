using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelector : MonoBehaviour
{
    #region public Variables
    public GameObject[] charaObjects;
    public Image readyIcon;
    public Image lockedIcon;
    public Vector2 selectionTreshold = new Vector2(-.5f, 0.5f);
    public float timeBetweenInputs = 0.1f;
    public PlayerManager playerManager;

    [HideInInspector] public int inputIndex = -1;
    [HideInInspector] public bool isReady;
    [HideInInspector] public int currentIndex;
    #endregion

    #region _private Variables
    GameObject[] _charaInstantiated;
    float _xAxis;
    int _intXAxis;
    int _currentXAxis;
    bool _waitingInput;
    float _currentTimer = -1;
    int _playerIndex;
    bool _canLock;
    #endregion

    void Start()
    {
        inputIndex = -1;
        _playerIndex = int.Parse(name.Split('_')[1]);
        _charaInstantiated = new GameObject[charaObjects.Length];
        for (int i = 0; i < _charaInstantiated.Length; i++)
        {
            _charaInstantiated[i] = Instantiate(charaObjects[i], transform);
            _charaInstantiated[i].transform.localPosition = Vector3.zero;
            _charaInstantiated[i].SetActive(false);
        }
    }

    void Update()
    {
        if (inputIndex < 0)
            return;

        updateAxis(timeBetweenInputs);

        if (!isReady)
        {
            //Check if someone else already has chose the character
            _canLock = playerManager.canLockChara(_playerIndex, currentIndex);
            if (!_canLock)
                lockedIcon.color = changeColorAlpha(lockedIcon.color, 1);
            else
                lockedIcon.color = changeColorAlpha(lockedIcon.color, 0);

            //Chara selection
            if (_currentXAxis < 0)
            {
                _charaInstantiated[currentIndex].SetActive(false);
                currentIndex = currentIndex <= 0 ? _charaInstantiated.Length - 1 : currentIndex - 1;
                _charaInstantiated[currentIndex].SetActive(true);
            }

            if (_currentXAxis > 0)
            {
                _charaInstantiated[currentIndex].SetActive(false);
                currentIndex = currentIndex >= _charaInstantiated.Length - 1 ? 0 : currentIndex + 1;
                _charaInstantiated[currentIndex].SetActive(true);
            }

            //Leave
            if (inputIndex > -1 && Input.GetButtonDown("Push_" + inputIndex))
            {
                _charaInstantiated[currentIndex].SetActive(false);
                currentIndex = 0;
                lockedIcon.color = changeColorAlpha(lockedIcon.color, 0);
                playerManager.RemovePlayer(_playerIndex, inputIndex);
            }

            //Ready
            if (inputIndex >= 0 && Input.GetButtonDown("Action_" + inputIndex) && _canLock)
            {
                readyIcon.color = changeColorAlpha(readyIcon.color, 1);
                isReady = true;
                playerManager.AddReady();
            }
        }
        else
        {
            if (Input.GetButtonDown("Push_" + inputIndex))
            {
                readyIcon.color = changeColorAlpha(readyIcon.color, 0);
                isReady = false;
                playerManager.RemoveReady();
            }
        }
    }

    void updateAxis(float time)
    {
        _xAxis = Input.GetAxisRaw("Horizontal_" + inputIndex);
        _intXAxis = _xAxis <= selectionTreshold.x ? -1 : _xAxis >= selectionTreshold.y ? 1 : 0;
        if (_intXAxis == 0 && _waitingInput)
            _waitingInput = false;

        if (_waitingInput)
        {
            _currentTimer -= Time.deltaTime;
            if (_currentTimer > 0)
            {
                _currentXAxis = 0;
            }
            else
                _waitingInput = false;
        }
        if (!_waitingInput && _intXAxis != 0)
        {
            _waitingInput = true;
            _currentXAxis = _intXAxis;
            _currentTimer = timeBetweenInputs;
        }
        if (!_waitingInput && _intXAxis == 0 && _currentXAxis != 0)
            _currentXAxis = 0;
    }

    public IEnumerator addPlayer(int _charaIndex)
    {
        int _index = inputIndex;
        inputIndex = -2;
        currentIndex = _charaIndex;
        _charaInstantiated[currentIndex].SetActive(true);
        yield return new WaitForSeconds(0.1f);
        inputIndex = _index;
        yield return null;
    }

    Color changeColorAlpha(Color _color, float _alpha)
    {
        return new Color(_color.r, _color.g, _color.b, _alpha);
    }
}