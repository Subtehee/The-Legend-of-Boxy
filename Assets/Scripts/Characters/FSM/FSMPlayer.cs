// ============================
// 수정 : 2021-06-17
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.State
{
    public enum MoveState
    {
        IDLE,
        RUN,
        JUMP,
        FALL,
        DOWNFALL,
        GLIDE,
        SPRINT,
        AUTOATTACK,
        STRONGATTACK,
        SKILLATTACK,
        DIE
    }

    public class FSMPlayer : MonoBehaviour
    {
        [SerializeField] private readonly FiniteStateMachine<FSMPlayer> FSM;



        protected void Awake()
        {

        }

    }

}
