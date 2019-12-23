using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Control : MonoBehaviour
{
    //public GameObject vb;
    public Animator ani;
    public VirtualButtonBehaviour[] vbs;
    public GameObject vb;

    public void OnButtonPressed(VirtualButtonBehaviour vb)
    {
        if (vb.VirtualButtonName == "button1")
        {
            ani.gameObject.transform.position += new Vector3(1, 0, 0);
        }
        else
        {
            ani.gameObject.transform.position += new Vector3(-1, 0, 0);
        }
        Debug.Log(vb.VirtualButtonName);
    }

    public void OnButtonReleased(VirtualButtonBehaviour vb)
    {
    }


    // Start is called before the first frame update
    void Start()
    {
        var vbs = GetComponentsInChildren<VirtualButtonBehaviour>();
        for (int i = 0; i < vbs.Length; ++ i)
        {
            vbs[i].RegisterOnButtonPressed(OnButtonPressed);
            vbs[i].RegisterOnButtonReleased(OnButtonReleased);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
