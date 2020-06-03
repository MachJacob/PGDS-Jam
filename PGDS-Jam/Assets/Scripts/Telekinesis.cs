using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : MonoBehaviour
{
    private Collider2D col;
    private List<Grabbable> dragged;
    public float TeleForce;
    [FMODUnity.EventRef] public string tkSustainAudioPath;
    FMOD.Studio.EventInstance tkAudioInstance;

    private void Start()
    {
        col = GetComponent<Collider2D>();
        dragged = new List<Grabbable>();
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
        transform.position = mousePos2D;
        if (Input.GetMouseButtonDown(0))
        {
            ContactFilter2D filter = new ContactFilter2D();
            List<Collider2D> list = new List<Collider2D>();
            if (Physics2D.OverlapCollider(col, filter, list) > 0)
            {
                foreach(Collider2D f in list)
                {
                    if (f.gameObject.GetComponent<Grabbable>())
                    {
                        dragged.Add(f.GetComponent<Grabbable>());
                        f.GetComponent<Grabbable>().SetDrag(true);
                        tkAudioInstance = FMODUnity.RuntimeManager.CreateInstance(tkSustainAudioPath);
                        FMODUnity.RuntimeManager.AttachInstanceToGameObject(tkAudioInstance, f.gameObject.transform, f.gameObject.GetComponent<Rigidbody2D>());
                        tkAudioInstance.start();
                    }
                }
            }
        }


        else if (Input.GetMouseButtonUp(0))
        {
            foreach(Grabbable grab in dragged)
            {
                tkAudioInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
                tkAudioInstance.release();
                grab.SetDrag(false);
            }
            dragged.Clear();
        }
        foreach(Grabbable grab in dragged)
        {
            grab.Drag(transform.position, TeleForce);

            float vel = grab.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude;

            //Caps velocity at 5 to be sent to FMOD
            vel = (vel > 5) ? 5 : vel;

            //Debug.Log(vel);

            tkAudioInstance.setParameterByName("Speed", vel);
        }
    }
}
