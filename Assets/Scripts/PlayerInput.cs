/* 20210604_���� �߰�, UpdateInput(), SetDashInput()
 * 
 */

using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerInput : MonoBehaviour
    {
        
        [SerializeField] private float moveAxisDeadZone = 0.2f;     // ������ Axis���� ����
        [SerializeField] private float dashDelay = 1.5f;            // �뽬 �� Ű ������ Ÿ��

        public Vector2 MoveInput { get; private set; }      // ���� �̵� Ű�Է�
        public Vector2 LastMoveInput { get; private set; }  // ���������� �Էµ� Ű�Է�
        public Vector2 CameraInput { get; private set; }    // ī�޶� ��Ʈ�� �Է�
        public bool JumpInput { get; private set; }         // ����Ű �Է�
        public bool DashInput { get; private set; }         // ���Ű �Է�
        public bool HasMoveInput { get; private set; }      // Ű�Է��� �޾Ҵ��� ����


        // �������Ӹ��� ����, ����� Ű�Է°� ����
        public void UpdateInput()
        {
            // ������̸� Ű�Է��� ���� ����
            if (DashInput)
                return;
            

            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // Ű �Է°��� DeadZone���̸� �Է°� 0���� �ʱ�ȭ
            if (Mathf.Abs(moveInput.x) < moveAxisDeadZone)
                moveInput.x = 0.0f;
            if (Mathf.Abs(moveInput.y) < moveAxisDeadZone)
                moveInput.y = 0.0f;

            // moveInput���� 0�̻��̸� true
            bool hasMoveInput = moveInput.sqrMagnitude > 0.0f;

            // Ű�Է��� ���� ���¿��� ���ο� Ű�Է��� ������ �ʾҴٸ� ������ �ִ� ���� ������ �Է°��� ��
            if (HasMoveInput && !hasMoveInput)
                LastMoveInput = moveInput;

            MoveInput = moveInput;          // MoveInput ������Ʈ
            HasMoveInput = hasMoveInput;    // Ű�� �Է� �޾Ҵ��� true / false

            CameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse y"));

            JumpInput = Input.GetButton("Jump");

            // ���� ���� �ƴ� ���� �뽬 ��ư �Է¹ޱ�
            if (!JumpInput)
                DashInput = Input.GetMouseButtonUp(1);
            // Dash Ű�� �ԷµǸ� ���� �ð� �������ֱ�
            if (DashInput)
                Invoke("SetDashInput", dashDelay);
        }

        // Dash�� ���� �� DashInput�� �ǵ�����
        private void SetDashInput()
        {
            DashInput = false;
        }
    }
}

