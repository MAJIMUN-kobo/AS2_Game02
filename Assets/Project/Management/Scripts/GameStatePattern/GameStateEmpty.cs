using UnityEngine;

public class GameStateEmpty : BaseGameState
{
    #region Constructor
    public GameStateEmpty(GameManager observer) : base(observer)
    {
        this.observer = observer;
    }
    #endregion
}
