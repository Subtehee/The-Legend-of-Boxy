using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerInput : MonoBehaviour
    {
        // ������ �ܿ� Ű�Է°� 
        [SerializeField] private float MoveAxisDeadZone = 0.2f;

        public Vector2 MoveInput { get; private set; }      // ���� �̵� Ű�Է�
        public Vector2 LastMoveInput { get; private set; }  // ���������� �Էµ� �̵� Ű�Է�
        public Vector2 CameraInput { get; private set; }    // ī�޶� �Է�
        public bool JumpInput { get; private set; }         // ����Ű �Է�
        public bool DashInput { get; private set; }         // �뽬Ű �Է�
        public bool HasMoveInput { get; private set; }      // Ű�Է��� �ް� �ִ��� ����

        public void UpdateInput()
        {
            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }

}

