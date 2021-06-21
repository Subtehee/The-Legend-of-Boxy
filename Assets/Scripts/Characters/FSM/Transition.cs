// ============================
// ���� : 2021-06-21
// �ۼ� : sujeong
// ============================

using System;

namespace Characters.FSM
{
    public class Transition
    {
        public Func<bool> Condition { get; set; }
        public IState To { get; set; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }

    }

}

