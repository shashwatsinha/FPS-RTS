using UnityEngine;
using System.Collections;

public class Selecting : MonoBehaviour {

    bool selected = false;
    public GameObject cam;
	// Use this for initialization
	void Start ()
    {
	    
	}
	
    void OnMouseOver()
    {
        if (CameraManager.rtsCamOn == true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("selected");
                selected = true;
                RTSCam.selectedUnit = gameObject;
              
               // EnemyProperties enemy = gameObject.GetComponent<EnemyProperties>();
               // enemy.Move();
            }
        }

        if (Input.GetKeyDown(KeyCode.P) && selected == true)
        {
      //      Debug.Log("pressed");
       //     CameraManager.rtsCamOn = false;
        //    cam.SetActive(true);
        }
    }
}
