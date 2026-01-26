using UnityEngine;
using ASProject;

public class PlayerStateUpdate : BaseCharacterState
{
    public PlayerStateUpdate(BaseCharacter observer) : base(observer)
    {
        this.observer = observer;
    }

    public override void StateEnter()
    {
        base.StateEnter();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void StatePause()
    {
        base.StatePause();
    }

    public override void StateExit()
    {
        base.StateExit();
    }
}
