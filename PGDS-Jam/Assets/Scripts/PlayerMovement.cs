using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public bool isDed;

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
        playerImpactAudioInstance = FMODUnity.RuntimeManager.CreateInstance(playerImapactAudioPath);
        playerImpactAudioInstance.setParameterByName("Force", collision.relativeVelocity.magnitude / 10);
        playerImpactAudioInstance.start();

        isDed = true;

        Debug.Log("You ded");
        //TODO: actually make ded

        playerImpactAudioInstance.release();
    }
}
