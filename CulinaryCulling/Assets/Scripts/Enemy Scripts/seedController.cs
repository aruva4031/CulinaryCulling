using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class seedController : MonoBehaviour {

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
		if(col.gameObject.tag == "Player")
		{
			//Damage Player
			Destroy(this.gameObject);
		}	
	}
}
