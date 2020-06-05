using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] Vector2 zoomRange;
    [SerializeField] float maxSpeed;
    private float targetZoom;

    void Update()
    {
        transform.position = player.position + offset;
        targetZoom = Mathf.Lerp(zoomRange.x, zoomRange.y,
            Mathf.Clamp(player.GetComponent<Rigidbody2D>().velocity.magnitude / maxSpeed, 0, 1));
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetZoom, Time.deltaTime);
    }
}
