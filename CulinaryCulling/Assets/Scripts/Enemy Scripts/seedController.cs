using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seedController : Pumpkin {

	public GameObject player;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnCollisionEnter(Collision col)
	{
		if(beFriended == true)
		{
			if(col.gameObject.tag == "Enemy")
			{
				col.gameObject.GetComponent<EnemyAI>().health--;
			}
		}
		else if(col.gameObject.tag == "Player")
		{
			//Damage Player
			col.gameObject.GetComponent<BefriendEnemy>().health--;
			Destroy(this.gameObject);
		}	
	}
}
