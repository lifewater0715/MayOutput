using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CartrigeCase : MonoBehaviour
{
    [Range(0f, 500f)]
    public float CartrigeSpeed;

    public float Timer = 5;

    void Start()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = transform.right * CartrigeSpeed;
    }
    void Update()
    {
        gameObject.transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
        Timer -= Time.deltaTime;
        if (gameObject.transform.localScale.x <= 0f)
        {
            
            Destroy(gameObject);
        }
    }
}
