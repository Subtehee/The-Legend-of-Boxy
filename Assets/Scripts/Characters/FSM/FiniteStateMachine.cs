// ============================
// 수정 : 2021-06-28
// 작성 : sujeong
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

        // 상태(타입)별 전환 목록 저장
        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        // 현재 실행중인 상태의 전환 가능 목록
        private List<Transition> _currentTransitions = new List<Transition>();
        // 어떤 상태에서도 전환 가능한 목록 저장
        private List<Transition> _anyTransitions = new List<Transition>();

        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        // Transition Tick
        public void UpdateState()
        {
            // FixedUpdate()가 한 번 실행된 후 상태 체크
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

            // 현재 실행중인 상태의 전환 가능 목록 가져오기
            _transitions.TryGetValue(CurrentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            CurrentState.Enter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            // 전환 목록이 있는지 확인
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            // 특정 상태의 전환 가능 목록 추가
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
