using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllyShooting : MonoBehaviour
{

    public GameObject player;
    public GameObject enemyUnit;
    public Transform bulletSpawnPoint;
    private LineRenderer laserShotLine;
    private SphereCollider col;
    PlayerMovement pProp;
    private float shootTimer;
    private float delayTimer;
    public GameObject projectile;
    public int health;
    public bool destroyed;
    private Queue<GameObject> friendlyQueue;
    EnemyShooting enemyProp;

    // Use this for initialization
    void Start()
    {
        
     //   player = GameObject.FindGameObjectWithTag("Player");
     //   enemyUnit = GameObject.FindGameObjectWithTag("EnemyUnits");
        laserShotLine = GetComponentInChildren<LineRenderer>();
        col = GetComponent<SphereCollider>();
        laserShotLine.enabled = false;
        pProp = GetComponent<PlayerMovement>();
        shootTimer = 1.0f;
        delayTimer = 0.0f;
        health = 2000;
        destroyed = false;
        friendlyQueue = new Queue<GameObject>();
    }

    void Update()
    {
        
        if (shootTimer < 0.0f)
        {
            delayTimer += Time.deltaTime;
        }

        if (delayTimer > 1.0f)
        {
            delayTimer = 0.0f;
            shootTimer = 1.0f;
        }



        if (friendlyQueue.Count > 0 && shootTimer > 0.0f && pProp.possessed == false)
             {
             //   GetEnemy();
                 Shoot();
             }
       // Debug.Log(friendlyQueue);
  /*      else
        {
            laserShotLine.enabled = false;
        }
        */
        if(destroyed==true)
        {
            Destroy(gameObject);
        }
    }

    void Shoot()
    {
        Debug.Log("Shoot");

          shootTimer -= Time.deltaTime;
        /*   if (enemyUnit != null)
           {
               laserShotLine.SetPosition(0, laserShotLine.transform.position);
               laserShotLine.SetPosition(1, enemyUnit.transform.position + Vector3.up);
               laserShotLine.enabled = true;
           }
           */
        if (friendlyQueue.Count>0)
        {
            var dir = enemyUnit.transform.position - transform.position;
        //    dir.y = 0;
            var rotation = Quaternion.LookRotation(dir);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2);
            Rigidbody instantiatedProjectile = Instantiate(projectile,
                                                       bulletSpawnPoint.transform.position,
                                                       transform.rotation) as Rigidbody;


            instantiatedProjectile.velocity = transform.TransformDirection(Vector3.forward * 100);
        }

        /*      if (enemyUnit != null)
              {
                  transform.LookAt(enemyUnit.transform);
                  GameObject go = (GameObject)Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);
                  go.GetComponent<Rigidbody>().AddForce(bulletSpawnPoint.forward * 5000);
              }
              */
        enemyProp.TakeDamage(10);
        if (enemyProp.health < 0)
        {
            enemyProp.destroyed = true;
            friendlyQueue.Dequeue();
            if (friendlyQueue.Count > 0)
            {
                enemyUnit = friendlyQueue.Peek();
                enemyProp = enemyUnit.GetComponent<EnemyShooting>();
            }
        }

    }

    public void TakeDamage(int t)
    {
        health = health - t;
      
    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Entered:" + col.name);
        if (col.gameObject.tag == "EnemyUnits" )
        {
            Debug.Log("here");
            friendlyQueue.Enqueue(col.gameObject);
            Debug.Log("Queue:"+friendlyQueue);
        }

        enemyUnit = friendlyQueue.Peek();

        enemyProp = enemyUnit.GetComponent<EnemyShooting>();

    }

    void OnTriggerExit(Collider col)
    {
        if(col.gameObject.tag == "EnemyUnits")
            friendlyQueue.Dequeue();
    }

    void GetEnemy()
    {
        if (friendlyQueue.Count > 0 && friendlyQueue != null)
        {
            enemyUnit = friendlyQueue.Peek();
            enemyProp = enemyUnit.GetComponent<EnemyShooting>();
        }
    }
}