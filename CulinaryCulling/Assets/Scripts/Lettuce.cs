using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lettuce : EnemyAI {

	Transform target;
	Rigidbody lettuceRB;

	public float coolDown;

	public float rotatingZ = 2.0f;
	// Use this for initialization
	void Start () {
		lettuceRB = this.GetComponent<Rigidbody>();
		target = GameObject.Find("Player").transform;
		speed = 5;
		health = 1;
		coolDown = 50;
	}
	
	// Update is called once per frame
	void Update () {
		coolDown--;
		if(coolDown <= 0)
		{
			Rotate();
			coolDown = 50;
		}
		
	}

	

	public void Rotate()
	{
		Vector3 offset = new Vector3(target.position.x,0,0);

		Debug.Log(rotatingZ);
		float step = speed * Time.deltaTime;
		Quaternion oldRot = transform.rotation;
		transform.rotation = Quaternion.Slerp(oldRot, transform.rotation, 2.0f);
		this.transform.position = Vector3.MoveTowards(transform.position, offset, step);
		//lettuceRB.AddForce(,ForceMode.Acceleration);
		if(transform.position.x < target.position.x)
		{
			lettuceRB.AddTorque(0.0f,0.0f,-2.0f, ForceMode.VelocityChange);
		}
		else if (transform.position.x > target.position.x){
			lettuceRB.AddTorque(0.0f,0.0f,2.0f, ForceMode.VelocityChange);
		}
	}

}
