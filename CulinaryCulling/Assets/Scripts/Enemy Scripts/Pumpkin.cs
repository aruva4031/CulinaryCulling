using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : EnemyAI {

	Transform target;
	public Rigidbody bulletSeed;
	
	public Transform mouth;

	private Rigidbody bulletInstance; 

	public Rigidbody seedPrefab;

	public bool inSight;

	public float coolDown = 100;

	public bool canShoot = true;

	public bool upperLevel;
	public BefriendEnemy bEnemy;




	// Use this for initialization
	void Start () {
		target = GameObject.Find("Player").transform;
		inSight = false;
		upperLevel = false;
	}
	
	public void OnTriggerEnter(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			inSight = true;
		}
	}

	public void OnTriggerExit(Collider col)
	{
		if(col.gameObject.tag == "Player")
		{
			inSight = false;
		}
	}

	// Update is called once per frame
	void Update () {
		// if(target.position.x > this.transform.position.x)
		// {
		// 	transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z);
		// }
		// else
		// {
		// 	transform.rotation = Quaternion.Euler (transform.rotation.x, -180, transform.rotation.z);
		// }

		//if(EnemyAI.transitionLevel < player.transform.positon.y)
		//{bool upper level = true;}

		//followPlayer();
		if(beFriended == true)
		{
			friend();
		}
		else
		{
			followPlayer(upperLevel);
			enemy();
		}
		/***
		if(befriended = true)
		{
			friend();
		}
		else
		{
			followPlayer();
			enemy();
		} */

	}
	public void enemy()
	{
		if(inSight)
		{
			//transform.LookAt(target.position);
			if(canShoot)
			{
				ShootSeed();
				canShoot = false;
			}
			coolDown--;
			if(coolDown <= 0)
			{
				coolDown = 100;
				canShoot = true;
			}
		}
	}

	public void friend()
	{
		//this.transform.position = new Vector3 (player.position.x - 1, player.transfrom.position.y,player.transfrom.position.z )
		if(inSight)
		{
			//transform.LookAt(target.position);
			if(canShoot)
			{
				ShootSeed();
				canShoot = false;
			}
			coolDown--;
			if(coolDown <= 0)
			{
				coolDown = 100;
				canShoot = true;
			}
		}
	}
	void ShootSeed()
	{
		bulletInstance = Instantiate(seedPrefab, mouth.position, mouth.rotation) as Rigidbody;
		bulletInstance.AddForce(mouth.right * 500);
	}

	public void followPlayer(bool upperLevel)
	{
		if(upperLevel)
		{
			if(target.position.x < this.transform.position.x)
			{
				transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z);
			}
			else
			{
				transform.rotation = Quaternion.Euler (transform.rotation.x, -180, transform.rotation.z);
			}
		}
		else
		{
			if(target.position.x > this.transform.position.x)
			{
				transform.rotation = Quaternion.Euler (transform.rotation.x, 0, transform.rotation.z);
			}
			else
			{
				transform.rotation = Quaternion.Euler (transform.rotation.x, -180, transform.rotation.z);
			}
		}
	}
}
