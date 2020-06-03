﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    private Collider2D col;
    private List<Grabbable> dragged;
    public float TeleForce;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        dragged = new List<Grabbable>();
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        transform.position = mousePos2D;
        if (Input.GetMouseButtonDown(0))
        {
            ContactFilter2D filter = new ContactFilter2D();
            List<Collider2D> list = new List<Collider2D>();
            if (Physics2D.OverlapCollider(col, filter, list) > 0)
            {
                foreach(Collider2D f in list)
                {
                    if (f.gameObject.GetComponent<Grabbable>())
                    {
                        dragged.Add(f.GetComponent<Grabbable>());
                        f.GetComponent<Grabbable>().SetDrag(true);
                    }
                }
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            foreach(Grabbable grab in dragged)
            {
                grab.SetDrag(false);
            }
            dragged.Clear();
        }
        foreach(Grabbable grab in dragged)
        {
            grab.Drag(transform.position, TeleForce);
        }
    }
}
