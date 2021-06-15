// ============================
// 수정 : 2021-06-15
// 작성 : sujeong
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

        /* 캐릭터별 FSM 정의 */

        protected IState currentState;
        protected IState PreviouseState;
        protected IState GlobalState;

        public void ChangeState()
        {

        }
    }
}
