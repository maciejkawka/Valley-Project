using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Okno : MonoBehaviour
{

    private int trigger = 0;
    private bool open = false;
    private Animator _animator = null;
    public Text press;
    private string OpenPatch = "event:/House/WindowOpen";
    private string ClosePatch = "event:/House/WindowClose";
    GameObject Forest;
    GameObject River;

    void Start()
    {
        _animator = GetComponent<Animator>();
        Forest = GameObject.Find("ForestSound");
        River = GameObject.Find("RiverSound");
    }

    void Update()
    {
        if (trigger == 1)
        {
            if (open == false) press.text = "Wbciśnij F aby otworzyć";
            if (open == true) press.text = "Wbciśnij F aby zamknąć";

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!open)
                {
                    _animator.SetBool("Isopen", true);
                    open = true;
                    FMODUnity.RuntimeManager.PlayOneShot(OpenPatch, this.transform.position);
                    press.text = "Wbciśnij F aby otworzyć";
                    
                    //FMOD
                    var script = Forest.GetComponent<Forest_Sound>();
                    var script1 = River.GetComponent<River_Sound>();
                    script.OpenWindow(true);
                    script1.OpenWindow(true);
                }

                else if (open)
                {
                    _animator.SetBool("Isopen", false);
                    open = false;
                    FMODUnity.RuntimeManager.PlayOneShot(ClosePatch, this.transform.position);
                    press.text = "Wbciśnij F aby zamknąć";


                    //FMOD
                    var script = Forest.GetComponent<Forest_Sound>();
                    var script1 = River.GetComponent<River_Sound>();

                    script.OpenWindow(false);
                    script1.OpenWindow(false);
                }
            }

            trigger = 0;
        }
    }

    void Triggered()
    {
        trigger = 1;
        press.enabled = true;
    }
}