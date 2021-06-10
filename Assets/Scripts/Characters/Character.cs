using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Characters
{

    public class Character : MonoBehaviour
    {
        [Header("MovementSetting")]
        public float moveSpeed = 10.0f;
        public float acceleration = 30.0f;
        public float sprintSpeed = 20.0f;
        public float jumpForce = 10.0f;

        [Header("GravitySetting")]
        public float groundedGravity = 5.0f;
        public float glidingGravity = 15.0f;
        public float downFallGravity = 30.0f;

        [Header("Health")]
        public int maxHP = 100;
        public int stamina = 40;

        protected Rigidbody m_Rigidbody = null;
        protected float currentSpeed = 0.0f;

        protected bool isGrounded = true;

        protected virtual void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {

        }
    }

}
