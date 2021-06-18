// ============================
// 수정 : 2021-06-17
// 작성 : sujeong
// ============================

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.State
{

    [CreateAssetMenu(fileName ="FiniteStateMachine", menuName ="FSM/FiniteStateMachine")]
    public class FiniteStateMachine<T> : ScriptableObject where T : MonoBehaviour
    {
        private T owner;
        private IState CurrentState = null;     // current state
        private IState PreviouseState = null;   // previouse state
        private IState GlobalState = null;      // Can transition from any state

        // 상태 전환 조건 목록


        public void Awake()
        {
            CurrentState = null;    
            PreviouseState = null;  
            GlobalState = null;     
        }

        public void UpdateState()
        {
            if (GlobalState != null) GlobalState.Execute();
            if (CurrentState != null) CurrentState.Execute();
        }

        // 상태 초기화
        public void Configure(T owner, IState initialState)
        {
            this.owner = owner;             // 실행 대상
            ChangeState(initialState);      // IDLE 또는 초기 상태
        }

        // 상태 변경
        public void ChangeState(IState NewState)
        {
            PreviouseState = CurrentState;
            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = NewState;
            if (NewState != null)
                CurrentState.Enter();
        }

        // 이전 상태로 되돌리기
        public void RevertToPreviouseState()
        {
            if (PreviouseState != null)
                ChangeState(PreviouseState);
        }
    }
}
