using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crickets_Sound : MonoBehaviour
{

    FMOD.Studio.EventInstance Crickiets;

    void Start()
    {
        Crickiets = FMODUnity.RuntimeManager.CreateInstance("event:/Outside/Crickets");
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(Crickiets, GetComponent<Transform>(), GetComponent<Rigidbody>());
        Crickiets.start();
    }
}
