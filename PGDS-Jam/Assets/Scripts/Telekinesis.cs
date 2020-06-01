using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    public float teleForce;
    private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    private bool drag;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        drag = false;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2d = new Vector2(mousePos.x, mousePos.y);
        if (Input.GetMouseButtonDown(0))
        {
            //if (Physics2D.Raycast(mousePos2d, Vector2.zero))
            if (Physics2D.OverlapPoint(mousePos2d) == GetComponent<BoxCollider2D>())
            {
                drag = true;
                rb.gravityScale = 0;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drag = false;
            rb.gravityScale = 1;
        }

        if (drag)
        {
            Vector2 dir = mousePos - transform.position;
            float dot = Vector2.Dot(rb.velocity, dir);
            float backforce = 1;
            if (dot < 0)
            {
                backforce = 4;
            }
            rb.AddForce(dir.normalized * backforce * teleForce * Time.deltaTime);
        }
    }
}
