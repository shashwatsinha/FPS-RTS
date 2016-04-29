using UnityEngine;
using System.Collections;

public class Moving : MonoBehaviour {

    public static GameObject selectedUnit;
    Vector3 newPosition;

    void Update()
    {
        if (CameraManager.rtsCamOn == true)
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

}
