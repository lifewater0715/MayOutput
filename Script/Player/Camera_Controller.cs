using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour
{
    public GameObject Gun; // 총
    public bool GunFire = false; //총 발사 여부

    public GameObject PlayerPoint; //플레이어 위치
    public Vector3 OriginalCamPos; // 카메라 위치
    public GameObject CamPos; //카메라 포인트
    public Transform CamTransform; //카메라 포인트 트렌스폼
    public Camera MainCam; // 메인 카메라

    public float OriginalCamSize = 5f; //카메라 크기
    public float CamIncrementValue; // 카메라 크기 조정값

    void Start()
    {
        OriginalCamPos = gameObject.transform.position;
        CamTransform = MainCam.transform;
        CamPos.transform.position = gameObject.transform.position;
    }
    void LateUpdate()
    {
        //카메라 포인트 지정
        MainCam.transform.position = Vector3.Lerp(transform.position, CamTransform.position, 2f * Time.smoothDeltaTime) + new Vector3(0f, 0f, -10f);
        //MainCam.transform.position = CamPos.transform.position + new Vector3(0f, 0f, -10f);

        // 총 반동 받아오기
        Gun = GameObject.FindWithTag("EqGun");
        GunFire = Gun.GetComponent<TestGun>().Is_Fire;

        if (Input.GetAxis("Mouse ScrollWheel") > 0 && Input.GetKey(KeyCode.LeftControl)) //카메라 확대
        {
            CamPos.transform.position += transform.up * 50 * Time.smoothDeltaTime;
            MainCam.orthographicSize += CamIncrementValue;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0 && Input.GetKey(KeyCode.LeftControl) && CamPos.transform.position.y >= 0f) //카메라 축소
        {
            CamPos.transform.position -= transform.up * 50 * Time.smoothDeltaTime;
            MainCam.orthographicSize -= CamIncrementValue;
        }
        //else if (Input.GetKey(KeyCode.LeftControl) == false && GunFire == false) //카메라 초기화
        //{
        //    CamPos.transform.position = PlayerPoint.transform.position;
        //    MainCam.orthographicSize = 5f;
        //}
    }
}
