using UnityEngine;

public interface IGameState
{
    void EnterState();
    void UpdateState();
    void ExitState();
}

public class BaseGameState : IGameState
{
    #region Properties
    public GameManager observer { get; protected set; }
    #endregion

    #region Constructor
    public BaseGameState(GameManager observer)
    {
        this.observer = observer;
    }
    #endregion

    #region Interface Methods
    public virtual void EnterState()
    {
        
    }

    public virtual void ExitState()
    {
        
    }

    public virtual void UpdateState()
    {
        
    }
    #endregion
}
