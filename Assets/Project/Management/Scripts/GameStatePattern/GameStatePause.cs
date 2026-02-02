using ASProject;
using UnityEngine;

public class GameStatePause : BaseGameState
{
    #region Constructor
    public GameStatePause(GameManager observer) : base(observer)
    {
        this.observer = observer;
    }
    #endregion

    public override void EnterState()
    {
        BaseCharacter[] characters = GameObject.FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);
        foreach(var c in characters)
        {
            if ((c as Player) != null)
                c.SetState(new PlayerStatePause(c));
            else if ((c as EnemyAI) != null)
                c.SetState(new EnemyStatePause(c));
        }

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
        BaseCharacter[] characters = GameObject.FindObjectsByType<BaseCharacter>(FindObjectsSortMode.None);
        foreach (var c in characters)
        {
            if (c == null) continue;

            if ((c as Player) != null)
                c.SetState(new PlayerStateUpdate(c));
            else if ((c as EnemyAI) != null)
                c.SetState(new EnemyStateUpdate(c));
        }

        base.ExitState();
    }
}
