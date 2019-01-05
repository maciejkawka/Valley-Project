using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sea_Sound : MonoBehaviour
{

    FMOD.Studio.EventInstance seaSound;
    FMOD.Studio.ParameterInstance Occluded;
    public GameObject _Player;
    public LayerMask OcclusionLayer = 1;

    void Start()
    {
        seaSound = FMODUnity.RuntimeManager.CreateInstance("event:/Outside/Sea");
        seaSound.getParameter("Occlusion", out Occluded);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(seaSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        seaSound.start();
    }

    private void FixedUpdate()
    {
        Occlusion();
        if (Vector3.Distance(this.transform.position, _Player.transform.position) > 70)
            seaSound.setPaused(true);   
        else
            seaSound.setPaused(false);    
    }

    void Occlusion()
    {
        bool col = Physics.Linecast(this.transform.position, _Player.transform.position, OcclusionLayer);
        if (!col)
            Occluded.setValue(0);
        

        else
            Occluded.setValue(1.0f);
    }
}
