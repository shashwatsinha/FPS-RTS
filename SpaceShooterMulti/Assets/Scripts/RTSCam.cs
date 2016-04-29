using UnityEngine;
using System.Collections;

public class RTSCam : MonoBehaviour {

    Vector3 xDir = new Vector3(1, 0, 0);
    Vector3 zDir = new Vector3(0, 0, 1);
    public GameObject fpsCam;
    float speed = 10.0f;


    public static GameObject selectedUnit;
    Vector3 newPosition;
    void Start()
    {
        CameraManager.rtsCamOn = true;
    }

    // Update is called once per frame
    void Update ()
    {
        if (CameraManager.rtsCamOn == true)
        {
            CameraMovement();
            UnitMovement();      
        }
    }

    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "Change Camera"))
        {
            CameraManager.rtsCamOn = false;
            //fpsCam.SetActive(true);
        }

    }

    void CameraMovement()
    {
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(xDir * Time.deltaTime * speed, Space.Self);
        }

        else if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(xDir * Time.deltaTime * -1 * speed, Space.Self);
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(zDir * Time.deltaTime * speed, Space.World);
        }

        else if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(zDir * Time.deltaTime * -1 * speed, Space.World);
        }
    }

    void UnitMovement()
    {
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                newPosition = hit.point;
                selectedUnit.transform.position = new Vector3(newPosition.x, selectedUnit.transform.position.y, newPosition.z);
            }
            selectedUnit = null;
        }
    }
}
