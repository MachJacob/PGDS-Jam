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
            if (Physics2D.Raycast(mousePos2d, Vector2.zero))
            {
                drag = true;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            drag = false;
        }

        if (drag)
        {
            Vector2 dir = mousePos - transform.position;
            rb.AddForce(dir * teleForce * Time.deltaTime);
        }
    }
}
