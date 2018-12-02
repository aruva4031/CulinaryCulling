using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableObject : MonoBehaviour {
	private bool circuit_on = false;
	public bool fan;
	public bool platform;
	public float speed;
	public GameObject platform_goal;
	private bool goalReached = false;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (circuit_on) {
			if (fan) {
				transform.Rotate (0,0,1.5f * speed);
			} 
			else if (platform) {
				
				if (!goalReached) {
					rb.AddForce (0, speed, 0);
				}
				if (transform.position.y >= platform_goal.transform.position.y) {
					goalReached = true;
					rb.AddForce (0, 0, 0);
				}
				if (transform.position.y <= platform_goal.transform.position.y) {
					goalReached = false;
				}
			}
			else {
				Debug.Log ("Need to select type of object");
			}

		}
	}
	public void Switch(bool power) {
		circuit_on = power;
	}
}
