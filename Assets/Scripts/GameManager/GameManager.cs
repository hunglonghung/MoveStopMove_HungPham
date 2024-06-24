using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State;
    public static event Action<GameState> OnGameStateChanged;
    [Header("User Data")]
    [SerializeField] public UserDataManager userDataManager;

    void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject); // Đảm bảo đối tượng không bị hủy khi tải cảnh mới
    }
    else
    {
        Destroy(gameObject); // Hủy đối tượng nếu đã có một thể hiện khác tồn tại
    }
}

    // Start is called before the first frame update
    void Start()
    {
        UpdateGameState(GameState.Home);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void UpdateGameState(GameState newState)
    {
        State = newState;
        switch (newState)
        {
            case GameState.Loading:
                break;
            case GameState.Home:
                break;
            case GameState.WeaponShop:
                break;
            case GameState.SkinShop:
                break;
            case GameState.GamePlay:
                break;
            case GameState.Settings:
                break;
            case GameState.Win:
                break;
            case GameState.Lose:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newState),newState,null);
        }
        OnGameStateChanged?.Invoke(newState);
    }

    public enum GameState{
        Loading,
        Home, 
        WeaponShop,
        SkinShop,
        GamePlay,
        Settings,
        Win,
        Lose,

        

    }
}