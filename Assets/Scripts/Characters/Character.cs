// ============================
// 수정 : 2021-07-01
// 작성 : sujeong
// ============================

using System.Collections;
using UnityEngine;
using Characters.FSM;
using Characters.FSM.Actions;

namespace Characters
{
    // 캐릭터의 상태 열거형
    [System.Serializable]
    public enum States
    {
        IDLE,           // 0
        RUN,            // 1
        SPRINT,         // 2
        DASH,           // 3
        JUMP,           // 4
        LANDING,        // 5
        FALL,           // 6
        DOWNFALL,       // 7
        CLIMB,          // 8
        GLIDE,          // 9
        AUTOATTACK,     // 10
        STRONGATTACK,   // 11
        SKILLATTACK,    // 12
        DIE             // 13
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        public Controller Controller = null;
        public LayerMask staticLayer = 0;

        [Header("Movement State")]
        public States State = States.IDLE;

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        [HideInInspector] public Rigidbody m_rigidbody = null;
        [HideInInspector] public float curSpeed = 0.0f;

        protected Animator m_animator = null;
        protected FiniteStateMachine FSM = null;

        protected float m_smoothVelocity = 0.0f;        // Restore SmoothRotate Velocity 
        protected float m_distanceFromGround = 0.0f;
        protected float m_animtaionDelay = 0.0f;
        protected float m_slopeRayOffset = 0.1f;
        protected bool Climbable = false;

        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();
            Controller ??= GetComponent<Controller>();

            if (staticLayer == 0)
                staticLayer = LayerMask.NameToLayer("STATICMESH");

            Controller.Character = this;
        }

        protected virtual void Update()
        {
            FSM.UpdateState();
            Debug.DrawRay(Controller.groundPos - moveDirection.normalized * m_slopeRayOffset, moveDirection, Color.blue);
        }

        protected virtual void LateUpdate()
        {
            m_distanceFromGround = Controller.GetDistanceFromGround();
        }

        protected virtual void FixedUpdate()
        {
            FSM.FixedUpdateState();
        }


        // override Behaviors //
        // 상태에 따라 달라지는 행동은 매개변수를 받음
        public virtual void UpdateMoveDirection() { }
        public virtual void OnRotate(float rotSpeed) { }
        public virtual void OnGravity(float gravity) { }
        public virtual void OnDecel() { }

        // Common Behaviors //
        public void OnMove(float moveSpeed, float accel)
        {
            float targetSpeed = Mathf.Lerp(curSpeed, moveSpeed, accel * Time.deltaTime);
            Vector3 _moveDirection = moveDirection.normalized;

            // 이동 가능한 상태에서 평지가 아닐 경우(오르막길, 내리막길) 움직일 방향의 각도 조절하기 
            if (Controller.Movable && Mathf.Abs(Controller.pointAngle) > float.Epsilon)
            {

                float angle = Controller.pointAngle;

                RaycastHit hit;
                if (Physics.Raycast(Controller.groundPos - _moveDirection.normalized * m_slopeRayOffset, 
                                    _moveDirection, out hit, 1.0f, staticLayer))
                {
                    // 속도는 지면의 기울기와 반비례

                    Debug.Log("Is Uphill");
                }
                else
                {
                    angle *= (-1.0f);
                    Debug.Log("Is Downhill");
                }

                Vector3 targetAngle = Vector3.Cross(_moveDirection, Vector3.up);     // moveDirection의 right벡터 (pitch의 기준 축)
                targetAngle *= angle;

                // 움직일 방향의 pitch 조정
                _moveDirection = Quaternion.Euler(targetAngle) * _moveDirection;

                Debug.DrawLine(transform.position + Vector3.up * 1.0f, transform.position + _moveDirection + Vector3.up * 1.0f, Color.red);


                // 한 축으로 했을 때 준나 잘됨 ex) X축일 경우
                //moveDirection = Quaternion.Euler(angle, 0.0f, 0.0f) * moveDirection;

            }

            Vector3 targetVelocity = _moveDirection * targetSpeed;
            curSpeed = targetSpeed;

            m_rigidbody.velocity = targetVelocity;
        }

        public void AddImpulseForce(Vector3 direction, float force)
        {
            m_rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        // Animation Funcs //
        public void ToAnimaition(int value)
        {
            // 중복 리턴
            if (m_animator.GetInteger("State") == value)
                return;

            m_animator.SetInteger("State", value);
        }

        public void SetAnimtaionDelay(float delayTime)
        {
            m_animtaionDelay = delayTime;
        }
    }
}
