using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.State
{
    public enum PlayerState
    {
        IDLE,
        RUN,
        JUMP,
        FALL,
        GLIDE,
        SPRINT,
        AUTOATTACK,
        STRONGATTACK,
        DIE
    }

    [CreateAssetMenu(fileName ="PlayerFSMHandler", menuName ="Character/FSM/PlayerFSMHandler")]
    public class PlayerFSMHandler : FSMHandler
    {

    }

}
