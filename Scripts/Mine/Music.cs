using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    FMOD.Studio.EventInstance musicInstance;
    FMOD.Studio.ParameterInstance parametr;
    float parametrValue = 0.1f;
 
    void Start()
    {
        musicInstance = FMODUnity.RuntimeManager.CreateInstance("event:/Music/Music_Main");
        musicInstance.getParameter("Music_Controller", out parametr);

        musicInstance.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(this.gameObject, this.GetComponent<Rigidbody>()));
        musicInstance.start();
        parametr.setValue(parametrValue);
    }

    public void SetValue(float value)
    {
        parametrValue = value;
        parametr.setValue(parametrValue);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ReverbZones")
        {
            string TriggerName = other.name;

            if (TriggerName == "HomeZone")
                SetValue(0.0f);
            else if (TriggerName == "ForestZone")
                SetValue(0.6f);
            else if (TriggerName == "ValleyZone" || TriggerName == "BeachZone")
                SetValue(0.9f);
        }

        if (other.name == "Bridge")
        {
            if (parametrValue == 0.1f)
                SetValue(0.4f);
            else
                SetValue(0.1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ReverbZones")
        {
            string TriggerName = other.name;

            if (TriggerName == "HomeZone")
                SetValue(0.1f);
            else if (TriggerName == "ValleyZone" || TriggerName == "ForestZone")
                SetValue(0.4f);
        }
    }
}
