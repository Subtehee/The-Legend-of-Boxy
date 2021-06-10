using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{

    public abstract class CharacterStatus : ScriptableObject
    {
        [Header("MovementSetting")]
        public float moveSpeed = 10.0f;
        public float acceleration = 30.0f;
        public float sprintSpeed = 20.0f;
        public float jumpForce = 10.0f;

        [Header("GravitySetting")]
        public float groundedGravity = 5.0f;
        public float downFallGravity = 30.0f;
        


    }

}
