using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_Controller : MonoBehaviour
{
    public Camera Cam;//카메라

    public Rigidbody2D PlayerRigidbody2D; // 플레이어 리지드바디

    public float PlayerHp = 465f; //플레이어 전체 체력

    public float PlayerHead_Hp = 35f; // 머리

    public float PlayerBody_Hp = 180f; // 몸

    public float PlayerR_Arm_Hp = 60f; //오른팔
    public float PlayerL_Arm_Hp = 60f; //왼팔

    public float PlayerR_Lag_Hp = 65f; //오른다리
    public float PlayerL_Lag_Hp = 65f; //왼다리

    public float PlayerMaxStamina = 100f; //최대 스테미나
    private float PlayerStamina = 100f; //스테미나

    public float PlayerSpeed = 100f; //플레이어 속력

    public float PlayerNomarSpeed = 100f; //플레이어 달리기 속력
    public float PlayerDeshSpeed = 260f; //플레이어 달리기 속력
    void Start()
    {
        PlayerRigidbody2D = gameObject.GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        Vector2 MousPos = Input.mousePosition;
        Vector2 Mouse = Cam.ScreenToWorldPoint(MousPos);
        //마우스 방향으로 바라보기
        if(Vector3.Distance(Mouse, transform.position) >= 1.07f) //마우스가 일정 거리안으로 들어오면 회전 멈춤
        {
            Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        //이동
        if (Input.GetKey(KeyCode.W))
        {
            PlayerRigidbody2D.AddForce(Vector2.up * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            PlayerRigidbody2D.AddForce(Vector2.down * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            PlayerRigidbody2D.AddForce(Vector2.left * PlayerSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            PlayerRigidbody2D.AddForce(Vector2.right * PlayerSpeed);
        }
        //달리기
        if(Input.GetKey(KeyCode.LeftShift))
        {
            PlayerSpeed = PlayerDeshSpeed;
        }
        else
        {
            PlayerSpeed = PlayerNomarSpeed;
        }
    }
}
