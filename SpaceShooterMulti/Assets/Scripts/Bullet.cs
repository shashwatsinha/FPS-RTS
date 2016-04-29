using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	void Start()
    {
        Destroy(gameObject,5);
    }
	// Update is called once per frame
	void Update ()
    {
        gameObject.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * 100);
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag=="EnemyUnits")
        {
            Destroy(gameObject);
        }
    }
}
