using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
	public GameObject[] ActivatedObjects;
	private bool circuit_on = false;
	public Material isOn;
	public Material isOff;
	private void Activate (bool power){
		foreach (GameObject platform in ActivatedObjects) {
			platform.GetComponent<ActivatableObject> ().Switch (power);
			//pass in power for objects to activate	
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision other)
	{
		if(other.transform.tag == "spatuala" || other.transform.tag == "fryingPan"){
			circuit_on = !circuit_on;
			if (circuit_on) {
				this.GetComponent<MeshRenderer> ().material = isOn;
			} 
			else {
				this.GetComponent<MeshRenderer> ().material = isOff;
			}
			Activate (circuit_on);
		}
	}
}
