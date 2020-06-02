using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lethalVel;
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > lethalVel)
        {
            Destroy(gameObject, 5);
            Debug.Log(collision.relativeVelocity.magnitude);
        }
    }
}
