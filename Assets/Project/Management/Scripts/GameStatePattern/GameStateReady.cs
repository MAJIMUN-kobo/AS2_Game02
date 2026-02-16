using ASProject;
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
        BaseCharacter[] characters = GameObject.FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);
        foreach (var c in characters)
        {
            if ((c as Player) != null)
                c.SetState(new PlayerStateUpdate(c));
            else if ((c as EnemyAI) != null)
                c.SetState(new EnemyStateUpdate(c));
        }

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

    public void OpenPauseUI()
    {
        
    }
}
