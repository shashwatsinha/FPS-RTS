using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    int health = 1000;
    public bool selected = false;
    float speed = 10.0f;
    bool rtsActive;
    public GameObject projectile;
    public GameObject spawnPos;
    float mainSpeed = 10f; // Regular speed.
    float shiftAdd = 25f;  // Multiplied by how long shift is held.  Basically running.
    float maxShift = 100f; // Maximum speed when holding shift.
    float camSens = .35f;  // Camera sensitivity by mouse input.
    private Vector3 lastMouse = new Vector3(Screen.width / 2, Screen.height / 2, 0); // Kind of in the middle of the screen, rather than at the top (play).
    private float totalRun = 1.0f;
    public bool possessed;

    void Start()
    {
        selected = false;
        rtsActive = true;
        possessed = false;
    }

    void Update()
    {
        if (CameraManager.rtsCamOn == false && selected == true)
        {
            // Mouse input.
            possessed = true;
            lastMouse = Input.mousePosition - lastMouse;
            lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
            lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
            transform.eulerAngles = lastMouse;
            lastMouse = Input.mousePosition;

            // Keyboard commands.
            Vector3 p = getDirection();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
                p = p * totalRun * shiftAdd;
                p.x = Mathf.Clamp(p.x, -maxShift, maxShift);
                p.y = Mathf.Clamp(p.y, -maxShift, maxShift);
                p.z = Mathf.Clamp(p.z, -maxShift, maxShift);
            }
            else {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }

            p = p * Time.deltaTime;
            Vector3 newPosition = transform.position;
            if (Input.GetKey(KeyCode.V))
            { //If player wants to move on X and Z axis only
                transform.Translate(p);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else {
                transform.Translate(p);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }

        }

        if (Input.GetKeyDown(KeyCode.P) && selected == true && rtsActive==true)
        {
            Debug.Log("pressed");
            CameraManager.rtsCamOn = false;
            rtsActive = false;
            possessed = true;
            GetComponent<Camera>().enabled = true;
            //   cam.SetActive(true);
        }

        else if (Input.GetKeyDown(KeyCode.P) && CameraManager.rtsCamOn == false && rtsActive == false)
        {
            Debug.Log("pressed");
            GetComponent<Camera>().enabled = false;
            CameraManager.rtsCamOn = true;
            possessed = false;
            selected = false;
            rtsActive = true;
            //   cam.SetActive(true);
        }

        mouseSelected();
    }


    void mouseSelected()
    {
        if (Input.GetMouseButtonDown(0))
        {
           Debug.Log("Mouse is down");

            RaycastHit hitInfo = new RaycastHit();
            bool hit = Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo);
            if (hit)
            {
              Debug.Log("Hit " + hitInfo.transform.gameObject.name);

                hitInfo.transform.gameObject.GetComponent<PlayerMovement>().selected = true;

                if (hitInfo.transform.gameObject.tag == "FriendlyUnits")
                {
                  Debug.Log("It's working!");
                }
                else {
                  Debug.Log("nopz");
                }
            }
            else {
             Debug.Log("No hit");
            }
            Debug.Log("Mouse is down");
        }
    }

    private Vector3 getDirection()
    {
        Vector3 p_Velocity = new Vector3();
        if (Input.GetKey(KeyCode.W))
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.R))
        {
            p_Velocity += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.F))
        {
            p_Velocity += new Vector3(0, -1, 0);
        }
        return p_Velocity;
    }

    public void resetRotation(Vector3 lookAt)
    {
        transform.LookAt(lookAt);
    }

   public void TakeDamage(int h)
    {
        health = health - h;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Health:" + health);
    }

   void Shoot()
    {
        Rigidbody instantiatedProjectile = Instantiate(projectile,
                                                       spawnPos.transform.position,
                                                       transform.rotation) as Rigidbody;


        instantiatedProjectile.velocity = transform.TransformDirection(Vector3.forward * 100);
    }



}


