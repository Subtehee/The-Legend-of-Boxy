using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // 움직임 제어 enu20210603
    enum MoveModeSwitching
    {
        Start,          // Nothing happening
        Impulse,        // 
        Acceleration,
        Force, 
        VelocityChange
    };

    MoveModeSwitching moveModeSwitching;

    Vector3 StartPos, StartForce;   // 움직임 시작 위치와 힘
    Vector3 NewForce;                 // 플레이어에게 적용되는 힘
    Rigidbody rigid;              // 플레이어의 강체

    string ForceXString = string.Empty;
    string ForceYString = string.Empty;

    float ForceX, ForceY;       // X, Y 좌표의 힘 크기
    float Result;                 // 결과 값?


    [SerializeField] private float moveDamping = 10.0f; // 움직임 제동
    [SerializeField] private float moveSpeed = 0.0f;    // 현재 이동 가능 속도
    [SerializeField] private float runSpeed = 10.0f;    // 달리기 속도    
    [SerializeField] private float dashSpeed = 20.0f;   // 대시 이동 속도
    [SerializeField] private float dashImpulse = 30.0f; // 대시 눌렀을 때의 순간 속도

    private float inputHor = 0;     // Horizontal 키 입력값
    private float inputVer = 0;     // Vertical 키 입력값
    private Vector3 moveVec = Vector3.zero;     // 플레이어가 움직이는 방향과 속도
    
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Start()
    {
        // 시작 상태로 초기화
        moveModeSwitching = MoveModeSwitching.Start;

        // 가해지는 힘 초기화
        NewForce = new Vector3(-5.0f, 1.0f, 0.0f);    

        // 실수값 초기화
        ForceX = 0.0f;
        ForceY = 0.0f;

        // 플레이어의 시작 위치와 Rigidbody의 위치 초기화
        StartPos = transform.position;
        StartForce = rigid.transform.position;
    }

    void Update()
    {
        // 방향키 입력
        inputHor = Input.GetAxisRaw("Horizontal");
        inputVer = Input.GetAxisRaw("Vertical");

        // 점프키 입력

        // 대시키 입력
    }

    private void FixedUpdate()
    {
        // 시작 상태가 아니거나 상태가 리셋되지 않았을 때 
        if(moveModeSwitching != MoveModeSwitching.Start)
        {
            NewForce = new Vector3(ForceX, ForceY, 0);
        }
    }

    // 상태에 따른 플레이어 움직임 함수_20210603
    private void Move()
    {
        switch (moveModeSwitching)
        {
            // Starting Mode, 시작 상태
            case MoveModeSwitching.Start:
                // 오브젝트와 강체의 위치 리셋
                transform.position = StartPos;
                rigid.transform.position = StartForce;

                // 강체의 속도 리셋
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
