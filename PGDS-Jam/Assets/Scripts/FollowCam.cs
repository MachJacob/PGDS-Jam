using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FollowCam : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] Vector2 zoomRange;
    [SerializeField] float maxSpeed;
    private float targetZoom;

    [FMODUnity.EventRef] public string musicPath;
    FMOD.Studio.EventInstance musicInstance;

    private PlayerMovement playerMovement;

    void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicPath);

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            musicInstance.setParameterByName("Level", 0);
        }
        else
		{
            musicInstance.setParameterByName("Level", 1);
        }
        musicInstance.start();

        GameObject playerObj = GameObject.Find("Player");
        playerMovement = playerObj.GetComponent<PlayerMovement>();
    }


    void Update()
    {
        transform.position = player.position + offset;
        targetZoom = Mathf.Lerp(zoomRange.x, zoomRange.y,
            Mathf.Clamp(player.GetComponent<Rigidbody2D>().velocity.magnitude / maxSpeed, 0, 1));
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetZoom, Time.deltaTime);

        if(playerMovement.isDed)
        {
            musicInstance.triggerCue();
            musicInstance.release();
        }
    }
}
