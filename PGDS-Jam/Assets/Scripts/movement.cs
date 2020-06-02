using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-Vector2.up * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-Vector2.right * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(Vector2.right * speed * Time.deltaTime);
        }
    }
}
