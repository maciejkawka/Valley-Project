using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_Sound : MonoBehaviour
{

    FMOD.Studio.EventInstance forestSound;
    FMOD.Studio.ParameterInstance Occluded;
    public GameObject _Player;
    public LayerMask OcclusionLayer = 1;
    bool windowOpened;

    void Start()
    {
        forestSound = FMODUnity.RuntimeManager.CreateInstance("event:/Outside/Forest");
        forestSound.getParameter("Occlusion", out Occluded);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(forestSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        forestSound.start();
    }

    private void FixedUpdate()
    {
        Occlusion();
    }

    void Occlusion()
    {
        bool col = Physics.Linecast(this.transform.position, _Player.transform.position, OcclusionLayer);

        if (!col)
            Occluded.setValue(0);
        else if (windowOpened && col)
            Occluded.setValue(0.5f);
        else
            Occluded.setValue(1.0f);
    }

    public void OpenWindow(bool isOpen)
    {
        windowOpened = isOpen;
    }
}
