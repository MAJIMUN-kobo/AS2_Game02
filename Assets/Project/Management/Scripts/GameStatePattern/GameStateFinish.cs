using UnityEngine;

public class GameStateFinish : BaseGameState
{
    #region Constructor
    public GameStateFinish(GameManager observer) : base(observer)
    {
        this.observer = observer;
    }
    #endregion
}
