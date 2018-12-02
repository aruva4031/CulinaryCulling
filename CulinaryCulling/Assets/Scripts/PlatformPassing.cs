using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPassing : MonoBehaviour {
	private bool onTwoway = false;
	private GameObject lastPlatform;
	public float fallingtime;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.S) && onTwoway){
			StartCoroutine (falling (fallingtime));
	
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "twoWayPlatform" || other.tag == "oneWayPlatform") {
			Physics.IgnoreCollision (other.GetComponent<Collider>(),GetComponent<Collider>());
		}
		else if(other.tag == "killzone"){
			//Code to restart
			//this.GetComponent<BefriendEnemy>().playerDies();
		}
		if(other.tag == "upperLevel") {

		}
		else if(other.tag == "lowerLevel") {

		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "twoWayPlatform" || other.tag == "oneWayPlatform") {
			Physics.IgnoreCollision (other.GetComponent<Collider>(),GetComponent<Collider>(),false);
		}
	}

	void OnCollisionEnter(Collision other) {
		if(other.transform.tag == "twoWayPlatform") {
			Debug.Log("Hit Platform");
			onTwoway = true;
			lastPlatform = other.transform.gameObject;
		}
	}

	void OnCollisionExit(Collision other){
		if(other.transform.tag == "twoWayPlatform") {
			Debug.Log ("Off Platform");
			onTwoway = false;
			//Physics.IgnoreCollision (lastPlatform.GetComponent<Collider>(),GetComponent<Collider>(),false);
			lastPlatform = null;
		}
	}

	public IEnumerator falling(float fallingTime){
		GameObject ignore = lastPlatform;
		Physics.IgnoreCollision (lastPlatform.GetComponent<Collider>(),GetComponent<Collider>());
		yield return new WaitForSeconds (fallingTime);
		Physics.IgnoreCollision (ignore.GetComponent<Collider>(),GetComponent<Collider>(), false);
	}
}
