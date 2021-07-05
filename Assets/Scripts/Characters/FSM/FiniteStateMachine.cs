// ============================
// ���� : 2021-06-29
// �ۼ� : sujeong
// ============================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.FSM
{

    public class FiniteStateMachine
    {
        private IState CurrentState = null;
        private IState PreState = null;

        private bool RunnableFixedUpdate = true;    // �ִϸ��̼� ��ȯ �����̿� ���� �������� �ֱ� ����
        private bool ExcuteFixedUpdate = true;      // ĳ���Ͱ� ������ �� ���� üũ�ϱ� ����

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
            // ������ȯ ��, FixedUpdate()�� �� �� ����Ǹ� ���� üũ ����
            if (ExcuteFixedUpdate)
            {
                var transition = GetTransition();
                if (transition != null)
                {
                    SetState(transition.To);

                    RunnableFixedUpdate = true;
                    ExcuteFixedUpdate = false;
                }
                CurrentState?.UpdateState();
            }
        }

        public void FixedUpdateState()
        {
            if (RunnableFixedUpdate)
            {
                CurrentState?.FixedUpdateState();
                if (!ExcuteFixedUpdate)
                    ExcuteFixedUpdate = true;
            }
            else
                PreState?.FixedUpdateState();
        }

        public void SetState(IState state)
        {
            if (state == CurrentState)
                return;

            PreState = CurrentState;

            // keep running FixedUpdate
            RunnableFixedUpdate = false;

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

            // ��ȯ ���� ���¿� ���� �߰�
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
