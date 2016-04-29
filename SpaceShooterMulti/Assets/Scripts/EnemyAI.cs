using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {
    NavMeshAgent agent;
    bool oldPosAt;
    Vector3 oldPos;
    Vector3 newPos;
    EnemyShooting enemy;
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent>();
        oldPos = transform.position;
        newPos = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z + 20);
        agent.destination = new Vector3(transform.position.x + 20, transform.position.y, transform.position.z + 20);
        oldPosAt = false;
        InvokeRepeating("Patrol", 5, 5);
        enemy = GetComponent<EnemyShooting>();
    }
	
	// Update is called once per frame
	void Patrol ()
    {
        Debug.Log(enemy.moving);
        if (enemy.moving == true)
        {
            if (oldPosAt == false )
            {

                agent.destination = oldPos;
                oldPosAt = true;
            }

            else
            {
                agent.destination = newPos;
                oldPosAt = false;
            }
        }
//
    //    else if(enemy.moving==false)
        {
   //         agent.Stop();
        }
       
	}
}
