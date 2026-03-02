using ASProject;
using System;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Classes
    public class SaveIntegerDetail
    {
        public string key;
        public int value;
    }
    #endregion

    #region Fields
    [SerializeField] private Transform _pauseMenuUGUI;

    private Player _player = null;
    private EnemyAI[] _enemies = null;
    private Item[] _items = null;
    #endregion

    #region Properties
    public BaseGameState currentGameState { get; private set; }
    public BaseGameState nextGameState { get; private set; }
    public BaseGameState previousGameState { get; private set; }
    public bool isGamePlaying { get; private set; } = false;
    public int diamondCollect { get; set; } = 0;
    public int gameScore { get; set; } = 0;
    public int gameHighScore { get; set; } = 0;

    public Player player
    {
        get
        {
            if (_player == null)
                _player = FindAnyObjectByType<Player>();

            return _player;
        }

        private set { _player = value; }
    }

    public EnemyAI[] enemies
    {
        get
        {
            if (_enemies == null)
                _enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);

            return _enemies;
        }

        private set { _enemies = value; }
    }

    public Item[] items
    {
        get
        {
            if (_items == null)
                _items = FindObjectsByType<Item>(FindObjectsSortMode.None);

            return _items;
        }

        private set { _items = value; }
    }
    #endregion

    #region Unity Methods
    void Start()
    {
        currentGameState = new GameStateEmpty(this);
        nextGameState = new GameStateEmpty(this);
        previousGameState = new GameStateEmpty(this);

        SetGameState(new GameStateReady(this));
    }

    void Update()
    {
        _GameStateUpdate();
    }
    #endregion

    #region Other Methods
    /// <summary>
    /// ゲームの初期化メソッド
    /// </summary>
    public void InitializeGame()
    {
        diamondCollect = 0;
        gameScore = 0;
    }

    /// <summary>
    /// 状態を設定・変更するメソッド
    /// </summary>
    /// <param name="next">次の状態</param>
    /// <param name="delay">(任意)切り替えまでの待ち時間</param>
    public void SetGameState(BaseGameState next, float delay = 0.0f)
    {
        nextGameState = next;
        Invoke("_SetGameStateInvoke", delay);
    }

    /// <summary>
    /// ゲーム開始メソッド
    /// </summary>
    public void OnGameBegin()
    {
        isGamePlaying = true;
    }

    /// <summary>
    /// ゲーム終了メソッド
    /// </summary>
    public void OnGameFinish()
    {
        isGamePlaying = false;
    }

    /// <summary>
    /// ポーズメニューの表示/非表示 変更
    /// </summary>
    /// <param name="active"></param>
    public void PauseMenuActivation(bool active)
    {
        _pauseMenuUGUI.gameObject.SetActive(active);
    }

    /// <summary>
    /// ダイヤの追加メソッド
    /// </summary>
    /// <param name="add">追加数</param>
    public void AddDiamond(int add)
    {
        diamondCollect += add;
        diamondCollect = Mathf.Max(0, diamondCollect);
    }

    /// <summary>
    /// 整数データの保存メソッド
    /// </summary>
    /// <param name="key">保存キー</param>
    /// <param name="data">保存値</param>
    public void SaveInteger(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 整数データのまとめて保存メソッド
    /// </summary>
    /// <param name="datas">セーブデータ配列</param>
    public void SaveIntegers(params SaveIntegerDetail[] datas)
    {
        foreach (var data in datas)
        {
            SaveInteger(data.key, data.value);
        }
    }

    /// <summary>
    /// セーブデータの読み込みメソッド
    /// </summary>
    /// <param name="key">保存キー</param>
    /// <returns>保存値</returns>
    public int LoadInt(string key)
    {
        try
        {
            return PlayerPrefs.GetInt(key);
        }
        catch (Exception e)
        {
            Debug.LogError($"セーブデータの読み込みに失敗しました。\nキー: {key}\nエラー内容: {e}");
            return -1;
        }
    }

    public void GamePlay()
    {
        BaseCharacter[] characters = GameObject.FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);
        foreach (var c in characters)
        {
            if (c == null) continue;

            if ((c as Player) != null)
                c.SetState(new PlayerStateUpdate(c));
            else if ((c as EnemyAI) != null)
                c.SetState(new EnemyStateUpdate(c));
        }

        PauseMenuActivation(false);
        var camera = GameObject.FindAnyObjectByType<CameraManager>();
        if (camera != null) camera.enabled = true;

        Time.timeScale = 1f;
    }

    public void GameStop()
    {
        BaseCharacter[] characters = GameObject.FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);
        foreach (var c in characters)
        {
            if ((c as Player) != null)
                c.SetState(new PlayerStatePause(c));
            else if ((c as EnemyAI) != null)
                c.SetState(new EnemyStatePause(c));
        }

        PauseMenuActivation(true);
        CursorSetActive(true);

        var camera = GameObject.FindAnyObjectByType<CameraManager>();
        if (camera != null) camera.enabled = false;

        Time.timeScale = 0f;
    }

    public void CursorSetActive(bool active)
    {
        if(active) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;

        Cursor.visible = active;
    }

    public void DebugDiamond()
    {
        diamondCollect = player.DiamondPurpose;
    }

    /// <summary>
    /// 状態を更新処理するメソッド
    /// </summary>
    private void _GameStateUpdate()
    {
        if (currentGameState != null)
            currentGameState.UpdateState();

        if (Input.GetKeyDown(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            DebugDiamond();
    }

    /// <summary>
    /// 状態を変更するInvoke用メソッド
    /// </summary>
    private void _SetGameStateInvoke()
    {
        currentGameState.ExitState();
        previousGameState = currentGameState;

        Debug.Log($"{currentGameState.GetType()} を終了しました。\n次に {nextGameState} へ遷移します。");

        currentGameState = nextGameState;
        currentGameState.EnterState();

        Debug.Log($"{currentGameState.GetType()} を開始します。");
    }


    #endregion
}