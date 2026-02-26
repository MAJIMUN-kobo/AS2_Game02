using UnityEngine;

public class GameStatePlay : BaseGameState
{
    #region Constructor
    public GameStatePlay(GameManager observer) : base(observer)
    {
        this.observer = observer;
    }
    #endregion

    public override void EnterState()
    {
        base.EnterState();

        observer.CursorSetActive(false);
    }

    public override void UpdateState()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            observer.SetGameState(new GameStatePause(observer));
        }

        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public void GameFinish()
    {
        observer.SetGameState(new GameStateFinish(observer), 1);
        observer.OnGameFinish();
        observer.CursorSetActive(true);
    }
}
