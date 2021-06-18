// ============================
// ���� : 2021-06-17
// �ۼ� : sujeong
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

        // ���� ��ȯ ���� ���


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

        // ���� �ʱ�ȭ
        public void Configure(T owner, IState initialState)
        {
            this.owner = owner;             // ���� ���
            ChangeState(initialState);      // IDLE �Ǵ� �ʱ� ����
        }

        // ���� ����
        public void ChangeState(IState NewState)
        {
            PreviouseState = CurrentState;
            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = NewState;
            if (NewState != null)
                CurrentState.Enter();
        }

        // ���� ���·� �ǵ�����
        public void RevertToPreviouseState()
        {
            if (PreviouseState != null)
                ChangeState(PreviouseState);
        }
    }
}
