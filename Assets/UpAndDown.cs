using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpAndDown : MonoBehaviour
{
    private CharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        ControllUp();
        ControllDown();
    }

    void ControllUp() {
        Vector3 pos = transform.position;
        if (
                OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)
            ) {
        }
    }

    void ControllDown() { 
        Vector3 pos = transform.position;
        if (
                OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch)
            ) {
        }
    }
}
