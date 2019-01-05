using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class River_Sound : MonoBehaviour
{

    public Vector3[] riverPosition = new Vector3[5];

    //FMOD
    FMOD.Studio.EventInstance riverSound;
    FMOD.Studio.ParameterInstance Occluded;
    public GameObject _Player;
    public LayerMask OcclusionLayer = 1;
    bool windowOpened;

    void Start()
    {
        riverSound = FMODUnity.RuntimeManager.CreateInstance("event:/Outside/River");
        riverSound.getParameter("Occlusion", out Occluded);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(riverSound, GetComponent<Transform>(), GetComponent<Rigidbody>());
        riverSound.start();
    }


    void Update()
    {
        if (Vector3.Distance(_Player.transform.position, this.transform.position) < 30)
        {
            bool paused;
            riverSound.getPaused(out paused);

            if (paused)
                riverSound.setPaused(false);

            Occlusion();

            for (int i = 0; i < riverPosition.Length - 1; i++)
            {
                if (_Player.transform.position.x < riverPosition[i].x && _Player.transform.position.x > riverPosition[i + 1].x)
                {
                    float a, b;

                    a = (riverPosition[i + 1].z - riverPosition[i].z) / (riverPosition[i + 1].x - riverPosition[i].x);
                    b = riverPosition[i + 1].z - a * riverPosition[i + 1].x;
                    this.transform.position = new Vector3(_Player.transform.position.x, this.transform.position.y, _Player.transform.position.x * a + b);
                }
            }
        }
        else
            riverSound.setPaused(true);
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
