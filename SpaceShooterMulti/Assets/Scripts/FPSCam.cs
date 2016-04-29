using UnityEngine;
using System.Collections;

public class FPSCam : MonoBehaviour {

    // Use this for initialization
    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Change Camera"))
        {
          //  gameObject.SetActive(false);
            CameraManager.rtsCamOn = true;
        }
    }
}
