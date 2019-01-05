using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class CubBoard : MonoBehaviour
{

    private int trigger = 0;
    private bool open = false;
    private Animator _animator = null;
    public Text press;
    private string OpenPath = "event:/House/CubboardOpen";
    private string ClosePath = "event:/House/CubboardClose";
    private string OpenPathDrawer = "event:/House/DrawerOpen";
    private string ClosePathDrawer = "event:/House/DrawerClose";


    void Start()
    {
        _animator = GetComponent<Animator>();

    }

    void Update()
    {
        if (trigger == 1)
        {

            if (open == false)
                press.text = "Wbciśnij F aby otworzyć";
            if (open == true)
                press.text = "Wbciśnij F aby zamknąć";

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (!open)
                {
                    _animator.SetBool("Isopen", true);
                    Play(open);
                    open = true;
                    press.text = "Wbciśnij F aby otworzyć";

                }
                else if (open)
                {
                    _animator.SetBool("Isopen", false);
                    Play(open);
                    open = false;
                    press.text = "Wbciśnij F aby zamknąć";
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

    void Play(bool isOpen)
    {
        if (isOpen)
        {
            if (this.name == "drawer_r") FMODUnity.RuntimeManager.PlayOneShot(ClosePathDrawer, this.transform.position);
            else FMODUnity.RuntimeManager.PlayOneShot(ClosePath, this.transform.position);
        }

        else
        {
            if (this.name == "drawer_r") FMODUnity.RuntimeManager.PlayOneShot(OpenPathDrawer, this.transform.position);
            else FMODUnity.RuntimeManager.PlayOneShot(OpenPath, this.transform.position);
        }
    }
}
