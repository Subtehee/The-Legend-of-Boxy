using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Climb : ActionBase
    {

        private readonly float _upSpeed = 0.0f;
        private readonly float _downSpeed = 0.0f;

        public PlayerAction_Climb(Character owner, States state)
            : base(owner, state)
        {

        }


    }

}


