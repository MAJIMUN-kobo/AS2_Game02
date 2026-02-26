using UnityEngine;
using ASProject;
using UnityEngine.UIElements.Experimental;

public class EnemyStateUpdate : BaseCharacterState
{
    public EnemyStateUpdate(BaseCharacter observer) : base(observer)
    {
        this.observer = observer;
    }

    public override void StateUpdate()
    {
        EnemyAI parent = observer as EnemyAI;
        if (parent == null) return;

        // ===== おとり中 =====
        if (parent.LuredUpdate()) return;

        // ===== 立ち止まって周囲を見る =====
        if (parent.LookingAroundUpdate()) return;

        // ===== スポナーへ帰還 =====
        if (parent.ReturningToSpawnUpdate()) return;

        // ===== 追跡していない =====
        parent.ChasingUpdate();

        parent.SmoothRotate();

        base.StateUpdate();
    }
}
