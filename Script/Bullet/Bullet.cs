using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Particle;
    public GameObject tracer;

    [Range(0f,500f)]
    public float BulletSpeed; //속도
    public float Piercing; //관통력

    public bool tracerbullet = false;

    public float Timer = 5;

    void Start()
    {
        if(tracerbullet == true)
        {
            tracer.SetActive(true);
        }
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.up * BulletSpeed; //임시 (추후에 관통비례 속도로 변경 예정)
    }
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) //관통 메커니즘
    {
        if (collision.tag == "Wall")
        {
            Particle.SetActive(true);
            int WallHard = collision.GetComponent<Wall>().Hard;

            if(WallHard >= Piercing)
                Destroy(gameObject);
            else
            {
                Piercing = Piercing - WallHard;
            }
        }
    }
}
