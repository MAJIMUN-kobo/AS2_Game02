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
        Player parent = observer as Player;

        parent.DestroyParticle();

        parent.SkillTimerUpdate();

        parent.PlayerMove();

        parent.ItemUse();


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
