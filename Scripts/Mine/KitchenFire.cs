using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenFire : MonoBehaviour
{

    ParticleSystem Fire = null;
    private bool Fired;
    private int trigger = 0;
    public Text Up;
    public Text Down;
    public GameObject Light = null;
    private float bolingTime = 0;
    FMOD.Studio.EventInstance KitchenUp;


    FMOD.Studio.ParameterInstance Tempo;

    private string UpPath = "event:/House/KitchenUp";




    void Start()
    {
        Fire = GetComponent<ParticleSystem>();
        Fired = Fire.isPlaying;
    }

    void Update()
    {
        bolingTime += Time.deltaTime;

        if (bolingTime >= 20)
            Tempo.setValue(0);

        if (trigger == 1)
        {
            if (!Fired)
            {
                Down.text = "Wciśnij F włączyć palnik";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    bolingTime = 0;
                    Fire.Play();
                    Fired = true;
                    Light.SetActive(true);

                    PlaySound(Fired);
                }
            }
            else if (Fired)
            {
                Down.text = "Wciśnij F zgasić palnik";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Fire.Stop();
                    Fired = false;
                    Light.SetActive(false);
                    PlaySound(Fired);
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
            KitchenUp = FMODUnity.RuntimeManager.CreateInstance(UpPath);
            KitchenUp.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject, GetComponent<Rigidbody>()));

            KitchenUp.getParameter("Tempo", out Tempo);
            Tempo.setValue(1);
            KitchenUp.start();
        }
        else
        {
            KitchenUp.release();
            KitchenUp.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }
    }
}
