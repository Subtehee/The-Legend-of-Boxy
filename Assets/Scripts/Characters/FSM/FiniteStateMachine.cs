// ============================
// 수정 : 2021-06-21
// 작성 : sujeong
// ============================

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.FSM
{
    public class FiniteStateMachine<T>
    {
        private T owner;
        private IState CurrentState = null;

        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        private List<Transition> _currentTransitions = new List<Transition>();
        private List<Transition> _anyTransitions = new List<Transition>();

        private static List<Transition> EmptyTransitions = new List<Transition>(0);
        
        public void UpdateState()
        {
            var transition = GetTransition();

            CurrentState?.UpdateState();
        }

        public void FixedUpdateState()
        {
            CurrentState?.FixedUpdateState();
        }

        public void SetState(IState state)
        {
            if (state == CurrentState)
                return;

            CurrentState?.Exit();
            CurrentState = state;

            _transitions.TryGetValue(_currentTransitions.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            CurrentState.Enter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate, Enum id)
        {
            // 
            if(_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            // Add new transition
            transitions.Add(new Transition(id, to, predicate));     
        }

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in _currentTransitions)
                if (transition.Condition())
                    return transition;

            return null;
        }
    }
}
