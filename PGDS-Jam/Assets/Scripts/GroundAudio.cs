using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundAudio : MonoBehaviour
{
    [FMODUnity.EventRef] public string groundImpactAudioPath;
    FMOD.Studio.EventInstance groundImpactAudioInstance;

    void Start()
    {
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        groundImpactAudioInstance = FMODUnity.RuntimeManager.CreateInstance(groundImpactAudioPath);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(groundImpactAudioInstance, gameObject.transform, gameObject.GetComponent<Rigidbody2D>());
        groundImpactAudioInstance.setParameterByName("Force", collision.relativeVelocity.magnitude / 10);
        groundImpactAudioInstance.start();
        groundImpactAudioInstance.release();
    }
}
