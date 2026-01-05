using UnityEngine;

public class GameStateReady : BaseGameState
{
    #region Constructor
    public GameStateReady(GameManager observer) : base(observer)
    {
        this.observer = observer;
    }
    #endregion

    public override void EnterState()
    {
        observer.SetGameState(new GameStatePlay(observer));

        base.EnterState();
    }

    public override void UpdateState()
    {
        base.UpdateState();
    }

    public override void ExitState()
    {
        base.ExitState();
    }
}
