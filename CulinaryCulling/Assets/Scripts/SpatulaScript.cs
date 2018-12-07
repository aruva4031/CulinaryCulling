using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpatulaScript : MonoBehaviour {

    public float tempTime = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("IN3");
        if (collision.gameObject.GetComponent<Collider>().tag == "Enemy")
        {
            Debug.Log("IN4");
            //if (collision.gameObject.GetComponent<EnemyAI>().health <= 0)
            //{
            //    Destroy(collision.gameObject);
            //}
            collision.gameObject.GetComponent<EnemyAI>().health -= 1;
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("IN2");
        if (collision.GetComponent<Collider>().gameObject.tag == "Enemy"&&(Time.time-tempTime>5f))
        {
            tempTime = Time.time;
            collision.gameObject.GetComponent<EnemyAI>().health--;
        }
    }
}
