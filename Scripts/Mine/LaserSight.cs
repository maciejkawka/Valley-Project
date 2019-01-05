using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LaserSight : MonoBehaviour
{

    public float distance = 4;
    public float Y_Offset = 0.8F;
    Vector3 pos;
    public Text text1;
    public Text text2;
    public GameObject Halo;

    public LayerMask IgnoreLayer = 1;

    void Update()
    {
        RaycastHit Hit;

        pos = new Vector3(transform.position.x, transform.position.y + Y_Offset, transform.position.z);
        if (Physics.Raycast(pos, transform.forward, out Hit, distance, IgnoreLayer))
        {
            if (Hit.collider.tag == "Triggerable")
                Hit.collider.SendMessage("Triggered");
            else
            {
                text1.enabled = false;
                text2.enabled = false;
                text1.text = "";
                text2.text = "";
            }
        }
        else
        {
            text1.enabled = false;
            text2.enabled = false;
            text1.text = "";
            text2.text = "";
        }
    }
}
