using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LargeMeatball : MonoBehaviour {
	public float delayOnSpawn;
	public float respawnDelay;
	private float start;
	private bool oneshot = false;
	private Vector3 respawnPoint;
	// Use this for initialization
	void Start () {
		respawnPoint = this.transform.position;
		start = Time.time + delayOnSpawn;
	}
	
	// Update is called once per frame
	void Update () {
		if (!oneshot && Time.time > start) {
			this.GetComponent<Collider> ().isTrigger = false;
			this.GetComponent<Rigidbody> ().isKinematic = false;
			oneshot = true;
		}
	}
	private void OnTriggerEnter(Collider other) {
		if(other.tag == "killzone"){
			StartCoroutine (meatballDrop(respawnDelay));
		}
	}
	private void Respawn()
	{
		this.transform.position = respawnPoint;
	}
	public IEnumerator meatballDrop(float delay){
		yield return new WaitForSeconds(delay);
		Respawn ();
	}
}
