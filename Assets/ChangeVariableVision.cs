using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVariableVision : MonoBehaviour {
    public Camera CenterEye;
    public static float DEFAULT_NEAR_PLANE = 0.1f;
    public static float DEFAULT_FAR_PLANE = 1000f;
    private bool isNarrowVision = false;
    private bool isPushingButton = false;
    // Start is called before the first frame update
    void Start() {
        //思ってたんと違
        //CenterEye.nearClipPlane = 3.5f;
        //CenterEye.farClipPlane = 50;

        // default 
        //CenterEye.rect.Set(0, 0, 1, 1);
        CenterEye.rect.Set(0, 0, 0.65f, 1);
    }

    // Update is called once per frame
    void Update() {
        if (
            Input.GetKeyDown(KeyCode.Space)
            ||
                OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch)
            ) {
            if (!isPushingButton) {
                if (isNarrowVision) {
                    CenterEye.rect.Set(0, 0, 1, 1);
                    isNarrowVision = false;
                } else {
                    CenterEye.rect.Set(0, 0, 0.65f, 1);
                    isNarrowVision = true;
                }
            }
            isPushingButton = true;
        } else {
            isPushingButton = false;
        }
    }
}
