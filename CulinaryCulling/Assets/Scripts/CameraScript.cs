using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {

    public Vector3 camLeftBorder;
    public Vector3 camRightBorder;
    public Vector3 camUpperBorder;
    public Vector3 camLowerBorder;

    public GameObject player;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LateUpdate()
    {
        transform.position = new Vector3(Mathf.Clamp(player.transform.position.x, camLeftBorder.x, camRightBorder.x), Mathf.Clamp(player.transform.position.y, camLowerBorder.y, camUpperBorder.y), transform.position.z);
    }
}
