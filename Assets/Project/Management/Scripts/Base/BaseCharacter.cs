using UnityEngine;

namespace ASProjrct
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        public BaseCharacterState currentGameState { get; private set; }
        public BaseCharacterState nextGameState { get; private set; }
        public BaseCharacterState previousGameState { get; private set; }

        void Start()
        {

        }

        void Update()
        {
            if (currentGameState != null)
                currentGameState.StateUpdate();
        }

        public void SetState(BaseCharacterState next)
        {
            currentGameState.StateExit();           // 現在の状態の終了処理を実行
            previousGameState = currentGameState;   // 現在の状態を保存

            Debug.Log($"{currentGameState.GetType()} を終了しました。\n次は {nextGameState} へ移行します。");

            currentGameState = nextGameState;       // 次の状態に移行
            currentGameState.StateEnter();          // 次の状態の開始処理を実行

            Debug.Log($"{currentGameState.GetType()} を開始します。");
        }


    }
}
