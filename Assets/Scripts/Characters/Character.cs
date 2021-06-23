// ============================
// 수정 : 2021-06-21
// 작성 : sujeong
// ============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.FSM;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        [HideInInspector] public Rigidbody rigid = null;
        [HideInInspector] public Animator anim = null;

        protected FiniteStateMachine FSM = null;

        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();
        }

        protected virtual void Update()
        {
            FSM.UpdateState();
        }

        protected virtual void FixedUpdate()
        {
            FSM.FixedUpdateState();
        }
    }
}
