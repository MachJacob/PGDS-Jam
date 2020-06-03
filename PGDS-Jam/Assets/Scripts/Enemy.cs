using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lethalVel;

    [FMODUnity.EventRef] public string explosionSoundPath;

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
            FMODUnity.RuntimeManager.PlayOneShotAttached(explosionSoundPath, gameObject);

            Destroy(gameObject, 5);
            //Debug.Log(collision.relativeVelocity.magnitude);
        }
    }
}
