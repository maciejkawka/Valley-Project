using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReverbZones : MonoBehaviour
{

    FMOD.Studio.EventInstance HomeZone;
    FMOD.Studio.EventInstance BeachZone;
    FMOD.Studio.EventInstance BasementZone;
    FMOD.Studio.EventInstance ValleyZone;
    FMOD.Studio.EventInstance ForestZone;
    FMOD.Studio.EventInstance DefaultZone;

    void Start()
    {
        HomeZone = FMODUnity.RuntimeManager.CreateInstance("snapshot:/House");
        BeachZone = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Beach");
        BasementZone = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Basement");
        ValleyZone = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Valley");
        ForestZone = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Forest");
        DefaultZone = FMODUnity.RuntimeManager.CreateInstance("snapshot:/Default");
    }


    void OnTriggerStay(Collider other)
    {
        if (other.tag == "ReverbZones")
        {
            string TriggerName = other.name;

            if (TriggerName == "HomeZone")
            {
                HomeZone.start();
                BeachZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                ValleyZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                ForestZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                BasementZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                DefaultZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }

            else if (TriggerName == "BasementZone")
            {
                BasementZone.start();
                BeachZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                ValleyZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                ForestZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                HomeZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                DefaultZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }

            else if (TriggerName == "ForestZone")
            {
                ForestZone.start();
                BeachZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                ValleyZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                BasementZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                HomeZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                DefaultZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }

            else if (TriggerName == "ValleyZone")
            {
                ValleyZone.start();
                BeachZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                ForestZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                BasementZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                HomeZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                DefaultZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }

            else if (TriggerName == "BeachZone")
            {
                BeachZone.start();
                ValleyZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                ForestZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                BasementZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                HomeZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
                DefaultZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            }
        }

        else
        {
            DefaultZone.start();
            ValleyZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            ForestZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            BasementZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            HomeZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            BeachZone.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }
}
