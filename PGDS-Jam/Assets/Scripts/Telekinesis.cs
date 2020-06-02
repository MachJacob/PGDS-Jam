﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    public float teleForce;
    private Rigidbody2D rb;
    private Camera cam;
    private LineRenderer lr;
    private bool drag;
    private Vector2 point;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        drag = false;

        cam = Camera.main;

        lr = gameObject.AddComponent<LineRenderer>();
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.startColor = new Color(205/255f, 90 / 255f, 214 / 255f, 150 / 255f);
        lr.endColor = new Color(205 / 255f, 90 / 255f, 214 / 255f, 150 / 255f);
        lr.startWidth = 0.025f;
        lr.endWidth = 0.025f;
    }

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);
        if (Input.GetMouseButtonDown(0))
        {
            //if (Physics2D.Raycast(mousePos2d, Vector2.zero))
            if (Physics2D.OverlapPoint(mousePos2d) == GetComponent<Collider2D>())
            {
                drag = true;
                rb.gravityScale = 0;
                point = mousePos - transform.position;
                lr.enabled = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drag = false;
            rb.gravityScale = 1f;
            lr.enabled = false;
        }

        if (drag)
        {
            lr.SetPosition(0, transform.TransformPoint(point));
            lr.SetPosition(1, mousePos2d);

            Vector2 dir = mousePos - transform.position;
            float dot = Vector2.Dot(rb.velocity, dir);
            float backforce = 1;
            if (dot < 0)
            {
                backforce = 4;
                //dir.Normalize();
            }
            rb.AddForceAtPosition(dir * backforce * teleForce * Time.deltaTime, transform.TransformPoint(point));
            //rb.AddForce(dir * backforce * teleForce * Time.deltaTime);
            //Debug.Log(dir);
        }
    }
}
