// ============================
// 수정 : 2021-06-29
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class ActionBase : IState
    {
        protected Character _owner = null;
        protected States _state;

        public ActionBase(Character owner, States state)
        {
            _owner = owner;
            _state = state;
        }

        public virtual void Enter() 
        {
            Debug.Log("Enter the State : " + _state.ToString());
            _owner.State = _state;
            _owner.ToAnimaition(_state.GetHashCode());
        }

        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
        public virtual void Exit() { }
    }

}

