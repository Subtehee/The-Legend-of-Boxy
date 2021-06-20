// ============================
// ���� : 2021-06-20
// �ۼ� : sujeong
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

        // ���� �ʱ�ȭ
        public void Configure(IState initialState)
        {
            ChangeState(initialState);      // IDLE �Ǵ� �ʱ� ����
        }

        // ���� ����
        public void ChangeState(IState NewState)
        {
            PreviouseState = CurrentState;
            CurrentState?.Exit();

            CurrentState = NewState;
            NewState?.Enter();
        }

        // ���� ���·� �ǵ�����
        public void RevertToPreviouseState()
        {
            if (PreviouseState != null)
                ChangeState(PreviouseState);
        }
    }
}
