using UnityEngine;
using System.Collections;

public class EnemyProperties : MonoBehaviour {

    int health;
    

    // Use this for initialization
    void Start ()
    {
        health = 1000;
	}
	
   public void TakeDamage(int t)
    {
        health = health - t;
        if(health<0)
        {
            Destroy(gameObject);
        }
    }


}
