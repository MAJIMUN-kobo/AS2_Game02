using System.Collections;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    #region Fields
    [SerializeField] private Transform pauseMenuUGUI;
    #endregion

    #region Properties
    public BaseGameState currentGameState { get; private set; }
    public BaseGameState nextGameState { get; private set; }
    public BaseGameState previousGameState { get; private set; }
    public bool isGamePlaying { get; private set; } = false;
    public int diamondCollect { get; set; } = 0;

    public Player player 
    { 
        get {
            if(player == null)
                player = FindAnyObjectByType<Player>();

            return player;
        }

        private set { player = value; } 
    }
    
    public EnemyAI[] enemies
    {
        get
        {
            if (enemies == null)
                enemies = FindObjectsByType<EnemyAI>(FindObjectsSortMode.None);

            return enemies;
        }

        private set { enemies = value; }
    }

    public Item[] items 
    {
        get 
        {
            if( items == null)
                items = FindObjectsByType<Item>(FindObjectsSortMode.None);

            return items;
        }

        private set { items = value; } 
    }
    #endregion

    #region Unity Methods
    void Start()
    {
        currentGameState  = new GameStateEmpty(this);
        nextGameState     = new GameStateEmpty(this);
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
    /// 状態を設定・変更するメソッド
    /// </summary>
    /// <param name="next">次の状態</param>
    /// <param name="delay">(省略可)遷移までの待ち時間</param>
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

    public void PauseMenuActivation(bool active)
    {
        pauseMenuUGUI.gameObject.SetActive(active);
    }

    /// <summary>
    /// 状態を更新し続けるメソッド
    /// </summary>
    private void _GameStateUpdate()
    {
        if(currentGameState != null)
            currentGameState.UpdateState();
    }

    /// <summary>
    /// 状態を変更するInvoke用メソッド
    /// </summary>
    private void _SetGameStateInvoke()
    {
        currentGameState.ExitState();           // 現在の状態の終了処理を実行
        previousGameState = currentGameState;   // 現在の状態を保存

        Debug.Log($"{currentGameState.GetType()} を終了しました。\n次は {nextGameState} へ移行します。");

        currentGameState = nextGameState;       // 次の状態に移行
        currentGameState.EnterState();          // 次の状態の開始処理を実行

        Debug.Log($"{currentGameState.GetType()} を開始します。");
    }
    #endregion
}
