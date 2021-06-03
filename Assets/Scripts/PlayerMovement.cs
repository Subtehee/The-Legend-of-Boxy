using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // ������ ���� enu20210603
    enum MoveModeSwitching
    {
        Start,          // Nothing happening
        Impulse,        // 
        Acceleration,
        Force, 
        VelocityChange
    };

    MoveModeSwitching moveModeSwitching;

    Vector3 StartPos, StartForce;   // ������ ���� ��ġ�� ��
    Vector3 NewForce;                 // �÷��̾�� ����Ǵ� ��
    Rigidbody rigid;              // �÷��̾��� ��ü

    string ForceXString = string.Empty;
    string ForceYString = string.Empty;

    float ForceX, ForceY;       // X, Y ��ǥ�� �� ũ��
    float Result;                 // ��� ��?


    [SerializeField] private float moveDamping = 10.0f; // ������ ����
    [SerializeField] private float moveSpeed = 0.0f;    // ���� �̵� ���� �ӵ�
    [SerializeField] private float runSpeed = 10.0f;    // �޸��� �ӵ�    
    [SerializeField] private float dashSpeed = 20.0f;   // ��� �̵� �ӵ�
    [SerializeField] private float dashImpulse = 30.0f; // ��� ������ ���� ���� �ӵ�

    private float inputHor = 0;     // Horizontal Ű �Է°�
    private float inputVer = 0;     // Vertical Ű �Է°�
    private Vector3 moveVec = Vector3.zero;     // �÷��̾ �����̴� ����� �ӵ�
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // ���� ���·� �ʱ�ȭ
        moveModeSwitching = MoveModeSwitching.Start;

        // �������� �� �ʱ�ȭ
        NewForce = new Vector3(-5.0f, 1.0f, 0.0f);    

        // �Ǽ��� �ʱ�ȭ
        ForceX = 0.0f;
        ForceY = 0.0f;

        // �÷��̾��� ���� ��ġ�� Rigidbody�� ��ġ �ʱ�ȭ
        StartPos = transform.position;
        StartForce = rigid.transform.position;
    }

    void Update()
    {
        // ����Ű �Է�
        inputHor = Input.GetAxisRaw("Horizontal");
        inputVer = Input.GetAxisRaw("Vertical");

        // ����Ű �Է�

        // ���Ű �Է�
    }

    private void FixedUpdate()
    {
        // ���� ���°� �ƴϰų� ���°� ���µ��� �ʾ��� �� 
        if(moveModeSwitching != MoveModeSwitching.Start)
        {
            NewForce = new Vector3(ForceX, ForceY, 0);
        }
    }

    // ���¿� ���� �÷��̾� ������ �Լ�_20210603
    private void Move()
    {
        switch (moveModeSwitching)
        {
            // Starting Mode, ���� ����
            case MoveModeSwitching.Start:
                // ������Ʈ�� ��ü�� ��ġ ����
                transform.position = StartPos;
                rigid.transform.position = StartForce;

                // ��ü�� �ӵ� ����
                rigid.velocity = new Vector3(0f, 0f, 0f);
                break;

            // Impulse Mode, 
            case MoveModeSwitching.Impulse:
                rigid.AddForce(NewForce, ForceMode.Impulse);
                break;

            case MoveModeSwitching.Acceleration:
                break;
            case MoveModeSwitching.Force:
                break;
            case MoveModeSwitching.VelocityChange:
                break;
            default:
                break;
        }
    }
}
