using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class HomeDataSaver : MonoBehaviour
{
    public static HomeDataSaver Instance { get; private set; }

    [SerializeField] private GameObject _playerContainer;
    [SerializeField] private GameObject _startGameWindow;
    [SerializeField] private VIDE_Assign _catBeginDialogue;
    [SerializeField] private SpriteRenderer _playerSpriteRenderer;
    public TopDownCharacterController move;

    private string _playerXPosKey = "playerXPos";
    private string _playerYPosKey = "playerYPos";
    private string _playerZPosKey = "playerZPos";
    private string _playerRotate = "playerRotate";
    private string _gameBeginned = "gameBeginned";

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _startGameWindow.SetActive(false);
        LoadHomeData();
    }

    public void SaveHomeData()
    {
        PlayerPrefs.SetInt(_playerRotate, _playerSpriteRenderer.flipX ? 1 : 0);
        PlayerPrefs.SetFloat(_playerXPosKey, _playerContainer.transform.position.x);
        PlayerPrefs.SetFloat(_playerYPosKey, _playerContainer.transform.position.y);
        PlayerPrefs.SetFloat(_playerZPosKey, _playerContainer.transform.position.z);
        Debug.Log(string.Format("saved: {0} {1} {2}", _playerContainer.transform.position.x, _playerContainer.transform.position.y, _playerContainer.transform.position.z));
    }

    public void LoadHomeData()
    {
        if (PlayerPrefs.HasKey(_playerXPosKey) && 
            PlayerPrefs.HasKey(_playerYPosKey) && 
            PlayerPrefs.HasKey(_playerZPosKey))
        {
            float x, y, z;
            x = PlayerPrefs.GetFloat(_playerXPosKey);
            y = PlayerPrefs.GetFloat(_playerYPosKey);
            z = PlayerPrefs.GetFloat(_playerZPosKey);
            _playerSpriteRenderer.flipX = PlayerPrefs.GetInt(_playerRotate) == 1;
            _playerContainer.transform.position = new Vector3(x, y, z);
            Debug.Log(string.Format("loaded: {0} {1} {2}", x, y, z));
        }

        if (PlayerPrefs.HasKey(_gameBeginned) && PlayerPrefs.GetInt(_gameBeginned) == 1)
        {
            TimeManager.Instance.StartTime();
        }
        else
        {
            _startGameWindow.SetActive(true);
        }
    }

    public void WaitCatDialog()
    {
        move.canMove = false;
        Invoke("StartCatDialog", 1);
    }

    private void StartCatDialog()
    {
        if (!VD.isActive)
        {
            VD.EndDialogue();
            VIDEUIManagerCustom.Instance.Interact(_catBeginDialogue);
        }
    }

    public void EndCatDialog()
    {
        PlayerPrefs.SetInt(_gameBeginned, 1);
        move.canMove = true;
    }

    private void OnApplicationQuit()
    {
        SaveHomeData();
    }
}
