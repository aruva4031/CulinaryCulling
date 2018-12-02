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





	// Use this for initialization
	void Start () {
		health = 10;
		speed = 0.5f;
		strt = new Vector3 (transform.position.x -1, transform.position.y, transform.position.z);
		nd = new Vector3 (transform.position.x + 1, transform.position.y, transform.position.z);
		coolDown = 500;
		transform.localScale = new Vector3(1,1,1);
		//StartCoroutine(Expanding(coolDown));
	}
	
	// Update is called once per frame
	void Update () {
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
	void FixedUpdate()
	{
		if(isOnPlatform())
		{
			moveOnPlatform(this.gameObject, platformLayer);
		}
		else
		{
			move(this.gameObject, strt, nd, speed,facingLeft);
		}
		// if(grow && canExpand)
		// {
		// if(canExpand)
		// {
		// 	if(grow)
		// 	{
		// 		size = Mathf.SmoothDampAngle(size,2.5f, ref speed, 5.0f);
		// 	}
		// 	if(size > 3)
		// 	{
		// 		size = Mathf.SmoothDampAngle(size,1.0f, ref speed, 5.0f);
		// 	}
		// 	//size = Mathf.SmoothDampAngle(size, (grow) ? 2.5f : 1.0f, ref speed, 5.0f);
		// 	transform.localScale = new Vector3(size,size,1);
		// }
		
		
			
		// }
		// else 
		// {
		// 	size = Mathf.SmoothDampAngle(size, 1f, ref speed, 5.0f);
		// 	transform.localScale = new Vector3(size,size,1);
		// 	speed = 0.5f;
		// }
	}
}
