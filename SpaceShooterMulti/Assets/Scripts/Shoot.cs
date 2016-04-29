using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

    public Camera mainCamera;
    public GameObject crossHair;
    public Transform bulletSpawn;
    public Transform playerPos;
    public GameObject bullet;
    Vector3 spawnPos;

    LineRenderer line;

    void Start()
    {
        line = gameObject.GetComponent<LineRenderer>();
        line.enabled = false;
    }
    void Update()
    {
        if (CameraManager.rtsCamOn == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StopCoroutine("FireLaser");
                StartCoroutine("FireLaser");
            }
        }
    }
    IEnumerator FireLaser()
    {
        line.enabled = true;

        while (Input.GetButton("Fire1"))
        {
            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;

            line.SetPosition(0, ray.origin);

            if (Physics.Raycast(ray, out hit, 100))
            {
                line.SetPosition(1, hit.point);
                
                if (hit.rigidbody)
                {
                    EnemyProperties enemy = hit.collider.GetComponent<EnemyProperties>();
                    Debug.Log("body hit" + hit.rigidbody.name);
                    enemy.TakeDamage(10);
                   // hit.rigidbody.AddForceAtPosition(transform.forward* 10, hit.point);
                }
            }
            else
                line.SetPosition(1, ray.GetPoint(100));

            yield return null;
        }

        line.enabled = false;
    }
}

