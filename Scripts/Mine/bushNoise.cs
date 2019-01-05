using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bushNoise : MonoBehaviour
{

    CapsuleCollider col;
    float mouseCordinate;
    private bool isSoftPlaying = false;
    private bool isHardPlaying = false;

    //FMOD

    FMOD.Studio.EventInstance softNoise;
    FMOD.Studio.EventInstance hardNoise;

    void Start()
    {
        col = GetComponent<CapsuleCollider>();
    }
    
    private void OnTriggerStay(Collider other)
    {
        var script = other.gameObject.GetComponent<PlayerMove>();

        if (other.tag == "Player")
        {
            script.BushCollider();
            BushSound(script.GetisMoving());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        hardNoise.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        softNoise.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        isSoftPlaying = false;
        isHardPlaying = false;
    }


    void BushSound(bool isMoving)
    {
        if (isMoving && !isHardPlaying)
        {
            hardNoise = FMODUnity.RuntimeManager.CreateInstance("event:/Outside/BushNoiseHard");
            hardNoise.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            hardNoise.start();
            hardNoise.release();
            isHardPlaying = true;
        }
        else if (!isMoving)
        {
            hardNoise.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            isHardPlaying = false;
        }

        if (MouseMovment() && !isSoftPlaying)
        {
            softNoise = FMODUnity.RuntimeManager.CreateInstance("event:/Outside/BushNoiseSoft");
            softNoise.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
            softNoise.start();
            softNoise.release();
            isSoftPlaying = true;
        }
        else if (!MouseMovment())
        {
            softNoise.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            isSoftPlaying = false;
        }
    }

    bool MouseMovment()
    {
        mouseCordinate = 0f;

        if (mouseCordinate - Input.GetAxis("Mouse X") == 0)
        {
            mouseCordinate = Input.GetAxis("Mouse X");
            return false;
        }
        else
        {
            mouseCordinate = Input.GetAxis("Mouse X");
            return true;
        }
    }
}
