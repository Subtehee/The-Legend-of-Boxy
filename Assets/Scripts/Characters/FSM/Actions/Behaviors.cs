// ============================
// 수정 : 2021-07-06
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    [CreateAssetMenu(fileName ="Behaviors", menuName ="FSM/Actions/Behaviors")]
    public class Behaviors : ScriptableObject
    {
        protected float m_smoothVelocity = 0.0f;

        public void OnRotate(Transform ownerTrans, Vector2 targetDirection, Quaternion curRotation, float rotSpeed) 
        {

            float targetAngle = Mathf.Atan2(targetDirection.x, targetDirection.y) * Mathf.Rad2Deg + curRotation.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(ownerTrans.eulerAngles.y, targetAngle, ref m_smoothVelocity, rotSpeed);

            ownerTrans.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }

        public void OnMove(Character owner, Rigidbody rigidbody, float moveSpeed, float accel, Vector3 moveDirection)
        {
            
            Vector3 targetVelocity = moveDirection * moveSpeed;
            rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity, targetVelocity, accel * Time.deltaTime);
        }

        public void OnGravity(Rigidbody rigidbody, float gravity)
        {

            rigidbody.AddForce(-Vector3.up * gravity);

            if (rigidbody.velocity.y < -gravity)
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, -gravity, rigidbody.velocity.z);
        }

        public void OnDecel(Rigidbody rigidbody, float deceleration)
        {

            // velocity deceleration
            rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity,
                new Vector3(0.0f, rigidbody.velocity.y, 0.0f), deceleration * Time.deltaTime);

            // Set Y velocity
            if (rigidbody.velocity.y > 0.0f)
                rigidbody.velocity = new Vector3(rigidbody.velocity.x, 0.0f, rigidbody.velocity.z);
        }

        public void AddImpulseForce(Rigidbody rigidbody, Vector3 direction, float force)
        {

            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }

}
