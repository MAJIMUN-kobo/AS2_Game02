using UnityEngine;

namespace ASProject
{
    public abstract class BaseCharacterState
    {
        public BaseCharacterState(BaseCharacter observer)
        {
            this.observer = observer;
        }

        public BaseCharacter observer { get; protected set; }

        public virtual void StateEnter()
        {

        }

        public virtual void StateUpdate()
        {

        }

        public virtual void StatePause()
        {

        }

        public virtual void StateExit()
        {

        }
    }
}