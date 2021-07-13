using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGun : MonoBehaviour
{
    public GameObject CamPos;
    public Vector3 originCamPos;

    public GameObject CartrigeCasePos;
    public GameObject CartrigeCase;

    public GameObject Muzzle;
    public GameObject Bullets;
    public float Rebound;
    public float Errorangle;

    public GameObject Flashlight;
    public bool FlashlightONOFF = false;

    public GameObject MuzzleFlame;
    public Transform SPR;
    public SpriteRenderer Spr;

    public Sprite None;
    public Sprite Sp1;
    public Sprite Sp2;

    [Range(0f,2000f)]
    public float RPM;
    private float RPM_;

    private float Timer = 0.02f;
    private float Timer_;

    public bool Is_Fire;

    void Start()
    {
        RPM = 6/((RPM / 60)*10)*2;
        RPM_ = RPM;
        Timer_ = Timer;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T) && FlashlightONOFF == false)
        {
            FlashlightONOFF = true;
            Flashlight.SetActive(false);
        }
        else if(Input.GetKeyDown(KeyCode.T) && FlashlightONOFF == true)
        {
            FlashlightONOFF = false;
            Flashlight.SetActive(true);
        }

        if (Is_Fire == true)
        {
            CamPos.transform.localPosition = (Vector3)Random.insideUnitCircle * Rebound + originCamPos;
        }
        else originCamPos = CamPos.transform.localPosition;

        MuzzleFlame.SetActive(false);
        Spr.sprite = None;
        if (Input.GetMouseButton(0) == true)
        {
            Is_Fire = false;
            RPM -= Time.deltaTime;
            if(RPM <= 0)
            {
                MuzzleFlame.SetActive(true);
                Timer -= Time.deltaTime;
                int Rand = Random.Range(0, 2);
                int Rand1 = Random.Range(0, 2);
                Is_Fire = true;
                if (Rand == 0)
                {
                    Spr.sprite = Sp1;
                }
                else
                {
                    Spr.sprite = Sp2;
                }

                if (Rand1 == 0)
                {
                    SPR.localScale = new Vector3(1, -1, 1);
                }
                else
                {
                    SPR.localScale = new Vector3(1, 1, 1);
                }
                if (Timer <= 0)
                {
                    RPM = RPM_;
                    Timer = Timer_;
                    
                    float r = Random.Range(Errorangle, -Errorangle);
                    GameObject bullet = GameObject.Instantiate(Bullets);
                    bullet.transform.position = Muzzle.transform.position;
                    bullet.transform.rotation = transform.rotation * Quaternion.AngleAxis(r, Vector3.forward);

                    GameObject cartrigeCase = GameObject.Instantiate(CartrigeCase);
                    cartrigeCase.transform.position = CartrigeCasePos.transform.position;
                    cartrigeCase.transform.rotation = transform.rotation * Quaternion.AngleAxis(r, Vector3.forward);
                }
            }
        }
        else
        {
            Is_Fire = false;
        }
    }
}
