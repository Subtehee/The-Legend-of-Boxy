// ============================
// ���� : 2021-06-15
// �ۼ� : sujeong
// ============================

using System.Collections.Generic;
using UnityEngine;

namespace Characters.State
{
    public interface IState
    {
        public void Enter();
        public void Execute();
        public void Exit();
    }

    public class FSMHandler : ScriptableObject
    {

        /* ĳ���ͺ� FSM ���� */

        protected IState currentState;
        protected IState PreviouseState;
        protected IState GlobalState;

        public void ChangeState()
        {

        }
    }
}
