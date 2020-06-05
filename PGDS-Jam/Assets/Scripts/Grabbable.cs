using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbable : MonoBehaviour
{
    private Rigidbody2D rb;
    private Camera cam;
    private LineRenderer lr;
    private Vector2 point;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main;

        lr = gameObject.AddComponent<LineRenderer>();
        lr = GetComponent<LineRenderer>();
        lr.material = new Material(Shader.Find("Legacy Shaders/Particles/Alpha Blended Premultiply"));
        lr.startColor = new Color(205 / 255f, 90 / 255f, 214 / 255f, 150 / 255f);
        lr.endColor = new Color(205 / 255f, 90 / 255f, 214 / 255f, 150 / 255f);
        lr.startWidth = 0.025f;
        lr.endWidth = 0.025f;
    }

    public void Drag(Vector3 _point, float _force)
    {
        lr.SetPosition(0, transform.TransformPoint(point));
        lr.SetPosition(1, _point);

        Vector2 dir = _point - transform.position;
        float dot = Vector2.Dot(rb.velocity, dir);
        float backforce = 1;
        if (dot < 0)
        {
            backforce = 4;
            //dir.Normalize();
        }
        rb.AddForceAtPosition(dir * backforce * _force * Time.deltaTime, transform.TransformPoint(point));
        //rb.AddForce(dir * backforce * teleForce * Time.deltaTime);
        //Debug.Log(dir);
    }

    public void SetDrag(bool _state)
    {
        lr.enabled = _state;
        if (_state)
        {
            rb.gravityScale = 0f;
        }
        else
        {
            rb.gravityScale = 1f;
        }

        point = cam.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }
}
