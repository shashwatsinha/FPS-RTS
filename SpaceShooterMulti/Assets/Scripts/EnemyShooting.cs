using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyShooting : MonoBehaviour {

    public GameObject player;
    public GameObject enemyUnit;
    public GameObject bullet;
    public Transform bulletSpawnPoint;
    private LineRenderer laserShotLine;
    private SphereCollider col;
    PlayerMovement pProp;
    private float shootTimer;
    private float delayTimer;
    public int health;
    public bool moving;
    private Queue<GameObject> friendlyQueue;
    AllyShooting enemyProp;
    public bool destroyed;
    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    //    enemyUnit = GameObject.FindGameObjectWithTag("FriendlyUnits");
        laserShotLine = GetComponentInChildren<LineRenderer>();
        col = GetComponent<SphereCollider>();
        laserShotLine.enabled = false;
        pProp = player.GetComponent<PlayerMovement>();
        shootTimer = 1.0f;
        delayTimer = 0.0f;
        moving = true;
        health = 1000000;
        destroyed = false;
        friendlyQueue = new Queue<GameObject>();
     //   friendlyQueue = null;
    }
	
    void Update()
    {
 
        if (shootTimer < 0.0f)
        {
            delayTimer += Time.deltaTime;
            moving = true;
        }

        if(delayTimer > 1.0f)
        {
            delayTimer = 0.0f;
            shootTimer = 1.0f;
            moving = true;
        }

       if(destroyed==true)
        {
            Destroy(gameObject);
        }

        if(friendlyQueue.Count>0 && shootTimer > 0.0f)
        {
            Shoot();
        }

        else
        {
            laserShotLine.enabled = false;
        }
    }

    void Shoot()
    {
         Debug.Log("Shoot");
 
          shootTimer -= Time.deltaTime;
              if (enemyUnit != null)
              {
                  laserShotLine.SetPosition(0, laserShotLine.transform.position);
                  laserShotLine.SetPosition(1, enemyUnit.transform.position + Vector3.up);
                  laserShotLine.enabled = true;
              }
      
  /*      if (enemyUnit != null)
        {
            transform.LookAt(enemyUnit.transform);
            GameObject go = (GameObject)Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
            go.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * 5000);
        }
        */
         enemyProp.TakeDamage(10);
         if(enemyProp.health<0)
        {
            enemyProp.destroyed = true;
            friendlyQueue.Dequeue();
            if(friendlyQueue.Count>0)
            {
                enemyUnit = friendlyQueue.Peek();
                enemyProp = enemyUnit.GetComponent<AllyShooting>();
            }
        }
       
    }

    public void TakeDamage(int t)
    {
        health = health - t;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "FriendlyUnits" || col.gameObject.tag == "Player")
        {
            friendlyQueue.Enqueue(col.gameObject);
        }
        enemyUnit = friendlyQueue.Peek();
        
        enemyProp = enemyUnit.GetComponent<AllyShooting>();
    }

    void OnTriggerExit(Collider col)
    {
        friendlyQueue.Dequeue();
    }
}
