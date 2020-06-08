using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float lethalVel;
    public Transform player;
    public float detRad;
    public GameObject bullet;
    private float cooldown;

    [FMODUnity.EventRef] public string explosionSoundPath;

    void Start()
    {
        cooldown = 5f;
    }

    void Update()
    {
        cooldown += Time.deltaTime;
        if (cooldown > 5 && Vector3.Distance(transform.position, player.position) < 5)
        {
            Vector2 dir = (player.position - transform.position).normalized;
            Vector3 spawn = transform.position + new Vector3((dir * 0.2f).x, (dir * 0.2f).y, 0);
            GameObject _bul = Instantiate(bullet, spawn, Quaternion.identity);
            _bul.GetComponent<Rigidbody2D>().AddForce(dir * 50);
            cooldown = 0;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.relativeVelocity.magnitude > lethalVel)
        {
            FMODUnity.RuntimeManager.PlayOneShotAttached(explosionSoundPath, gameObject);
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;

            Destroy(gameObject, 5);
            //Debug.Log(collision.relativeVelocity.magnitude);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, detRad);
        //Matrix4x4 oldMatrix = Gizmos.matrix;
        //Gizmos.color = new Color(0.2f, 0.2f, 0.2f, 0.5f); //this is gray, could be anything
        //Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(1, 1, 1));
        //Gizmos.DrawSphere(Vector3.zero, detRad);
        //Gizmos.matrix = oldMatrix;
    }
}
