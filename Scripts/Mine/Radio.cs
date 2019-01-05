using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Radio : MonoBehaviour
{

    private int trigger = 0;
    private bool Radio_On = false;
    public Text press;
    public Text press2;

    FMOD.Studio.EventInstance _Radio;

    FMOD.Studio.ParameterInstance Radiouz;
    FMOD.Studio.ParameterInstance Radiouz1;
    FMOD.Studio.ParameterInstance Radiouz2;
    FMOD.Studio.ParameterInstance RadioVolume;
    FMOD.Studio.ParameterInstance RadioNoise;
    FMOD.Studio.ParameterInstance Luz1Parameter;
    FMOD.Studio.ParameterInstance Luz2Parameter;
    FMOD.Studio.ParameterInstance LuzParameter;
    FMOD.Studio.ParameterInstance NoiseParameter;
    FMOD.Studio.ParameterInstance VolumeParameter;

    private float Volume = 0f;

    private float LuzValue = 1;
    private float Luz1Value = 0;
    private float Luz2Value = 0;
    private float NoiseValue = 0;
   
    public  LayerMask OcclusionLayer = 1;
    public LayerMask ShellLayer = 1;
    public GameObject _Player;
    FMOD.Studio.ParameterInstance OcclusionParameter;
    private string RadioPath = "event:/House/Radio";

    void Start ()
    {
        _Radio = FMODUnity.RuntimeManager.CreateInstance(RadioPath);
        
        _Radio.getParameter("RadioLuz", out LuzParameter);
        _Radio.getParameter("RadioLuz1", out Luz1Parameter);
        _Radio.getParameter("RadioLuz2", out Luz2Parameter);
        _Radio.getParameter("RadioNoise", out NoiseParameter);
        _Radio.getParameter("RadioVolume", out VolumeParameter);
        _Radio.getParameter("Occlusion", out OcclusionParameter);

        VolumeParameter.setValue(Volume);
        Luz1Parameter.setValue(Luz1Value);
        Luz2Parameter.setValue(Luz2Value);
        LuzParameter.setValue(LuzValue);
        NoiseParameter.setValue(NoiseValue);

        _Radio.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject, GetComponent<Rigidbody>()));
        _Radio.start();        
    }

    private void FixedUpdate()
    {
        Occlusion();   
    }
 
    void Update ()
    {	
        if(trigger ==1)
        {
            if(Radio_On)
            {
                press.enabled = true;
                press2.enabled = true;
                press.text = "F - Wyącz Radio    G - Zmień Stacje";
                press2.text = "R - Ciszej    T - Głośniej";

                if (Input.GetKeyDown(KeyCode.F))
                {
                    Radio_On = false;
                    Volume = 0;
                }

                else if (Input.GetKeyDown(KeyCode.R))
                {
                    if (Volume >= 0.1f)
                        Volume -= 0.1f;                  
                }

                else if (Input.GetKeyDown(KeyCode.T))
                {
                    if(Volume!=1)
                        Volume += 0.1f;
                }
            }

            else if(!Radio_On)
            {
                press.enabled = true;
                press2.enabled = true;

                press2.text = "F - Włącz radio";
                press.text = "";

                if(Input.GetKeyDown(KeyCode.F))
                {
                    Radio_On = true;
                    Volume = 0.8f;
                }
            }
          
                VolumeParameter.setValue(Volume);
                Luz1Parameter.setValue(Luz1Value);
                Luz2Parameter.setValue(Luz2Value);
                LuzParameter.setValue(LuzValue);
                NoiseParameter.setValue(NoiseValue);                                
        }

        trigger = 0;

	}

    void Triggered()
    {
        trigger = 1;
        press.enabled = true;
    }

    void Occlusion()
    {
        bool col = Physics.Linecast(this.transform.position, _Player.transform.position, OcclusionLayer);
        bool shell = Physics.Linecast(this.transform.position, _Player.transform.position, ShellLayer);

        if (!shell)
        {
            if (!col)
            {
                OcclusionParameter.setValue(0);        
            }

            else
            {
                OcclusionParameter.setValue(0.5f);        
            }
        }

        else
        {
            OcclusionParameter.setValue(1);     
        }       
    }
}
