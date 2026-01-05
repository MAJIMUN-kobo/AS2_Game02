using UnityEngine;

public class GameStatePause : BaseGameState
{
    #region Constructor
    public GameStatePause(GameManager observer) : base(observer)
    {
        this.observer = observer;
    }
    #endregion
}
