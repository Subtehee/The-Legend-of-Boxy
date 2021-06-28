// ============================
// ���� : 2021-06-28
// �ۼ� : sujeong
// ============================

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.FSM
{
    public class FiniteStateMachine
    {
        private IState CurrentState = null;
        private bool ExcuteFixedUpdate = false;     // Wait for FixedUpdate

        // ����(Ÿ��)�� ��ȯ ��� ����
        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        // ���� �������� ������ ��ȯ ���� ���
        private List<Transition> _currentTransitions = new List<Transition>();
        // � ���¿����� ��ȯ ������ ��� ����
        private List<Transition> _anyTransitions = new List<Transition>();

        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        // Transition Tick
        public void UpdateState()
        {
            // FixedUpdate()�� �� �� ����� �� ���� üũ
            if (ExcuteFixedUpdate)
            {
                
                var transition = GetTransition();
                if (transition != null)
                {
                    ExcuteFixedUpdate = false;
                    SetState(transition.To);
                }
                CurrentState?.UpdateState();
            }
        }

        public void FixedUpdateState()
        {
            CurrentState?.FixedUpdateState();
            ExcuteFixedUpdate = true;
        }

        public void SetState(IState state)
        {
            if (state == CurrentState)
                return;

            CurrentState?.Exit();
            CurrentState = state;

            // ���� �������� ������ ��ȯ ���� ��� ��������
            _transitions.TryGetValue(CurrentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            CurrentState.Enter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            // ��ȯ ����� �ִ��� Ȯ��
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            // Ư�� ������ ��ȯ ���� ��� �߰�
            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {
            _anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in _currentTransitions)
            {
                if (transition.Condition())
                    return transition;
            }

            return null;
        }
    }
}
