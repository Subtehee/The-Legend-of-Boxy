using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerInput : MonoBehaviour
    {
        // 제외할 잔여 키입력값 
        [SerializeField] private float MoveAxisDeadZone = 0.2f;

        public Vector2 MoveInput { get; private set; }      // 평지 이동 키입력
        public Vector2 LastMoveInput { get; private set; }  // 마지막으로 입력된 이동 키입력
        public Vector2 CameraInput { get; private set; }    // 카메라 입력
        public bool JumpInput { get; private set; }         // 점프키 입력
        public bool DashInput { get; private set; }         // 대쉬키 입력
        public bool HasMoveInput { get; private set; }      // 키입력을 받고 있는지 여부

        public void UpdateInput()
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

}

