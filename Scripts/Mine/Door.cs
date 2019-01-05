using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {

    private int trigger = 0;
    private bool open = false;
    private Animator _animator = null;
    public Text press;
    private GameObject Forest;
    private string OpenPath = "event:/House/DoorOpen";
    private string ClosePath = "event:/House/DoorClose";

  

    void Start()
    {     
        _animator = GetComponentInParent<Animator>();
        Forest = GameObject.Find("ForestSound");
    }

    void FixedUpdate() {

        if (trigger == 1)
        {         
            if(open==false)
                press.text = "Wbciśnij F aby otworzyć";
            if(open == true)
                press.text = "Wbciśnij F aby zamknąć";
            if (Input.GetKeyDown(KeyCode.F))
            {              
                if (!open)
                {
                    _animator.SetBool("Isopen", true);
                    open = true;
                    FMODUnity.RuntimeManager.PlayOneShot(OpenPath, this.transform.position);
                    press.text = "Wbciśnij F aby otworzyć";

                    //FMOD
                    var script = Forest.GetComponent<Forest_Sound>();
                    script.OpenWindow(true);
                }
                else if(open)
                {
                    _animator.SetBool("Isopen", false);
                    open = false;
                    FMODUnity.RuntimeManager.PlayOneShot(ClosePath, this.transform.position);
                    press.text = "Wbciśnij F aby zamknąć";

                    //FMOD
                    var script = Forest.GetComponent<Forest_Sound>();
                    script.OpenWindow(false);
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
