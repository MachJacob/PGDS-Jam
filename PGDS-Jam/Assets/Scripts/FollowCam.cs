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
    public bool title = false;

    [FMODUnity.EventRef] public string musicPath;
    FMOD.Studio.EventInstance musicInstance;

    private PlayerMovement playerMovement;

    private Portal portalCom;

    void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance(musicPath);

        Debug.Log(SceneManager.GetActiveScene().name);

        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            musicInstance.setParameterByName("Level", 2);
        }
        else if(SceneManager.GetActiveScene().name == "SampleScene 1")
		{
            musicInstance.setParameterByName("Level", 1);
        }
        else
        {
            musicInstance.setParameterByName("Level", 0);
        }
        musicInstance.start();

        GameObject playerObj = GameObject.Find("Player");
        playerMovement = playerObj.GetComponent<PlayerMovement>();

        GameObject portalObj = GameObject.Find("portal");
        portalCom = portalObj.GetComponent<Portal>();
    }


    void Update()
    {
        if (title)
        {
            transform.Translate(3 * Time.deltaTime, 0, 0);
            return;
        }
        transform.position = player.position + offset;
        targetZoom = Mathf.Lerp(zoomRange.x, zoomRange.y,
            Mathf.Clamp(player.GetComponent<Rigidbody2D>().velocity.magnitude / maxSpeed, 0, 1));
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, targetZoom, Time.deltaTime);

        if(portalCom.isActive)
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }

        if(playerMovement.isDed)
        {
            musicInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            musicInstance.release();
        }
    }
}
