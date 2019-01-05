using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirePlace : MonoBehaviour
{

    ParticleSystem Fire = null;
    private bool Fired;
    private int trigger = 0;
    public Text Up;
    public Text Down;
    public GameObject Light = null;
    public GameObject Light2 = null;
    public GameObject Light3 = null;
    FMOD.Studio.EventInstance FireUp;
    FMOD.Studio.EventInstance FireDown;
    private string DownPath = "event:/House/FireDown";
    private string UpPath = "event:/House/FireUp";

    public LayerMask OcclusionLayer = 1;
    public LayerMask ShellLayer = 1;
    public GameObject _Player;
    FMOD.Studio.ParameterInstance OcclusionParameter;

    
    void Start()
    {
        Fire = GetComponent<ParticleSystem>();
        Fired = true;


        FireUp = FMODUnity.RuntimeManager.CreateInstance(UpPath);
        FireDown = FMODUnity.RuntimeManager.CreateInstance(DownPath);

        FireUp.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject, GetComponent<Rigidbody>()));
        FireDown.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject, GetComponent<Rigidbody>()));

        FireUp.getParameter("Occlusion", out OcclusionParameter);

        PlaySound(true);
    }

    void FixedUpdate()
    {
        if (Fired)
            Occlusion();
    }

    void Update()
    {

        if (trigger == 1)
        {
            if (!Fired)
            {
                Down.text = "Wciśnij F aby rozniecić ogień";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Fire.Play();
                    Fired = true;
                    Light.SetActive(true);
                    Light2.SetActive(true);
                    if (Light3 != null) Light3.SetActive(true);

                    PlaySound(true);
                }
            }

            else if (Fired)
            {
                Down.text = "Wciśnij F aby zgasić ogień";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Fire.Stop();
                    Fired = false;
                    Light.SetActive(false);
                    Light2.SetActive(false);
                    if (Light3 != null)
                        Light3.SetActive(false);
                    PlaySound(false);
                }
            }
        }
        trigger = 0;
    }

    void Triggered()
    {
        trigger = 1;
        Down.enabled = true;
    }

    void PlaySound(bool isFire)
    {
        if (isFire)
        {
            FireDown.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            FireUp.start();
        }
        else
        {
            FireDown.start();
            FireUp.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    void Occlusion()
    {
        bool col = Physics.Linecast(this.transform.position, _Player.transform.position, OcclusionLayer);
        bool shell = Physics.Linecast(this.transform.position, _Player.transform.position, ShellLayer);

        if (!shell)
        {
            if (!col)
                OcclusionParameter.setValue(0);
            else
                OcclusionParameter.setValue(0.5f);
        }
        else
            OcclusionParameter.setValue(1);
    }
}



