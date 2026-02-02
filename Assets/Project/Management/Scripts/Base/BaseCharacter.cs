using UnityEngine;

namespace ASProject
{
    public abstract class BaseCharacter : MonoBehaviour
    {
        public BaseCharacterState currentCharacterState { get; private set; }
        public BaseCharacterState nextCharacterState { get; private set; }
        public BaseCharacterState previousCharacterState { get; private set; }

        protected virtual void Start()
        {

        }

        protected virtual void Update()
        {
            if (currentCharacterState != null)
            {
                //Debug.Log($"{currentCharacterState}を実行中");
                currentCharacterState.StateUpdate();
            }
        }

        public void SetState(BaseCharacterState next)
        {
            if (currentCharacterState != null)
            {
                currentCharacterState.StateExit();           // 現在の状態の終了処理を実行
                previousCharacterState = currentCharacterState;   // 現在の状態を保存
            }

            Debug.Log($"{currentCharacterState?.GetType()} を終了しました。\n次は {next} へ移行します。");

            currentCharacterState = next;       // 次の状態に移行
            currentCharacterState.StateEnter();          // 次の状態の開始処理を実行

            Debug.Log($"{currentCharacterState.GetType()} を開始します。");
        }


    }
}
