// ============================
// 수정 : 2021-06-20
// 작성 : sujeong
// ============================

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.FSM
{
    public class FiniteStateMachine
    {
        private IState CurrentState = null;     // current state
        private IState PreviouseState = null;   // previouse state
        private IState GlobalState = null;      // Can translate from any state
        
        public void Awake()
        {
            CurrentState = null;    
            PreviouseState = null;  
            GlobalState = null;     
        }

        public void UpdateState()
        {
            GlobalState?.UpdateState();
            CurrentState?.UpdateState();
        }

        public void FixedUpdateState()
        {
            GlobalState?.FixedUpdateState();
            GlobalState?.FixedUpdateState();
        }

        // 상태 초기화
        public void Configure(IState initialState)
        {
            ChangeState(initialState);      // IDLE 또는 초기 상태
        }

        // 상태 변경
        public void ChangeState(IState NewState)
        {
            PreviouseState = CurrentState;
            CurrentState?.Exit();

            CurrentState = NewState;
            NewState?.Enter();
        }

        // 이전 상태로 되돌리기
        public void RevertToPreviouseState()
        {
            if (PreviouseState != null)
                ChangeState(PreviouseState);
        }
    }
}
