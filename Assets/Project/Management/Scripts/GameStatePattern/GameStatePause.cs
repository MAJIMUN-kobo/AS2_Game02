using ASProject;
using UnityEngine;

public class GameStatePause : BaseGameState
{
    private CameraManager _camera;

    #region Constructor
    public GameStatePause(GameManager observer) : base(observer)
    {
        this.observer = observer;
    }
    #endregion

    public override void EnterState()
    {
        observer.GameStop();

        base.EnterState();
    }

    public override void UpdateState()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            observer.SetGameState(new GameStatePlay(observer));
        }

        base.UpdateState();
    }

    public override void ExitState()
    {
        observer?.GamePlay();

        base.ExitState();
    }
}
