using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public bool isDed;
    [SerializeField] private float health;

    [FMODUnity.EventRef] public string playerImapactAudioPath;
    FMOD.Studio.EventInstance playerImpactAudioInstance;


    //
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isDed = false;
    }

    void Update()
    {
        if (isDed) return;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (!isDed)
        {
            health--;

            if (health <= 0)
            {
                isDed = true;

                Debug.Log("You ded");
                rb.gravityScale = 1;
                rb.constraints = RigidbodyConstraints2D.None;
                rb.drag = 0.5f;

                playerImpactAudioInstance = FMODUnity.RuntimeManager.CreateInstance(playerImapactAudioPath);
                playerImpactAudioInstance.setParameterByName("Force", collision.relativeVelocity.magnitude / 10);
                playerImpactAudioInstance.start();
                playerImpactAudioInstance.release();
            }
        }
    }
}
