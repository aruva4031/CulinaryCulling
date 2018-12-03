using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomato : EnemyAI {

	bool onPlatform;
	
	bool facingLeft = true;
	public LayerMask platformLayer;

	bool grow = false;

	public float size = 1;

	public bool canExpand = true;
	public float coolDown;

	public Transform target;



	// Use this for initialization
	void Start () {
		health = 10;
		speed = 0.5f;
		strt = new Vector3 (transform.position.x -1, transform.position.y, transform.position.z);
		nd = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
		coolDown = 500;
		//transform.localScale = new Vector3(1,1,1);
		target = GameObject.Find("Player").transform;
		//StartCoroutine(Expanding(coolDown));
	}
	
	// Update is called once per frame
	void Update () {
		if(beFriended == true)
		{

		}
		// if(grow)
		// {
		// 	coolDown--;
		// 	canExpand = false;
		// }

		// if(coolDown <= 0)
		// {
		// 	coolDown = 500;
		// 	canExpand = true;
		// }
		// if(coolDown >= 0 && grow)
		// {
		// 	canExpand = false;
		// }

		// if(coolDown <= 0)
		// {
		// 	coolDown = 500;
		// 	canExpand = true;
		// }
	}

	void OnTriggerEnter(Collider col)
	{
		if(canExpand)
		{
			if(col.GetComponent<Collider>().gameObject.tag == "Player")
				{
					grow = true;
				}
				else
				{
					grow = false;
				}
		}

	}

	public bool isOnPlatform()
	{
		Vector3 position = transform.position;
		Vector3 direction = Vector3.down;
		float distance = 1.0f;
		
		if(Physics.Raycast(position, direction, distance, platformLayer))
		{
			return true;
		}
		return false;
	}

	// IEnumerator Expanding(float duration)
	// {
	// 	yield return new WaitForSeconds(duration);

	// }

	// public void expandTomato()
	// {

	// }
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
		}	
	}
	public void enemy()
	{
		if(isOnPlatform())
		{
			moveOnPlatform(this.gameObject, platformLayer);
		}
		else
		{
			move(this.gameObject, strt, nd, speed,facingLeft);
		}
	}
	public void friend()
	{
		Vector3 offset = new Vector3(target.position.x-2, target.position.y, target.position.z);
		this.transform.position = Vector3.MoveTowards(transform.position, offset, 0.5f);
		move(this.gameObject,strt,nd,speed,facingLeft);
	}
	void FixedUpdate()
	{
		
		if(beFriended == true)
		{
			friend();
			this.gameObject.tag = "Friend";
		}
		else{
			enemy();
		}
	}
}
