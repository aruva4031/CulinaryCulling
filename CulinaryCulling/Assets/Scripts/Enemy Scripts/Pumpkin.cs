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
		health = 2;
	}
	
	public void OnTriggerEnter(Collider col)
	{
		if(beFriended == true)
		{
			if(col.gameObject.tag == "Enemy")
			{
				inSight = false;
			}
		}
		else if(col.gameObject.tag == "Player")
		{
			inSight = true;
		}
	}

	public void OnTriggerExit(Collider col)
	{
		if(beFriended == true)
		{
			if(col.gameObject.tag == "Enemy")
			{
				inSight = false;
			}
		}
		else if(col.gameObject.tag == "Player")
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
			this.gameObject.tag = "Friend";
			this.GetComponent<BoxCollider>().enabled = false;
		}
		else
		{
			followPlayer(upperLevel);
			enemy();
		}
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
		Vector3 offset = new Vector3(target.position.x-2, target.position.y, target.position.z);
		this.transform.position = Vector3.MoveTowards(transform.position, offset, 0.5f);
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
