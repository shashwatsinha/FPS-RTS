using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public GameObject fpsCam;
    public GameObject rtsCam;
    public static bool rtsCamOn = true;

    // Use this for initialization
    void Start ()
    {
        rtsCamOn = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(rtsCamOn==true)
        {
            rtsCam.SetActive(true);
            fpsCam.SetActive(false);
        }

        else
        {
            rtsCam.SetActive(false);
            fpsCam.SetActive(true);
        }
	
	}
}
