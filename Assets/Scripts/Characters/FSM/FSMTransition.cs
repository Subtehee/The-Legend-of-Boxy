// ============================
// ���� : 2021-06-20
// �ۼ� : sujeong
// ============================

using System;

namespace Characters.FSM
{
    public class FSMTransition
    {
        public Func<bool> Condition { get; set; }
        public IState To { get; set; }

        public FSMTransition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }

    }

}

