using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector3 leftBorder;
    public Vector3 rightBorder;
    public Vector3 upperBorder;
    public Vector3 lowerBorder;

    public GameObject spatula;
    public GameObject fryingPan;
    public Camera cam;
    bool panWithPlayer;
    bool playerJumps;

    Vector3 direction;

    private enum playerState { IDLE,WALKING, JUMPING, SPATULA_ATTACK, FRYING_PAN_ATTACK };
    private playerState current_state = playerState.IDLE;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        spatula = transform.GetChild(0).gameObject;
        fryingPan = transform.GetChild(1).gameObject;
        panWithPlayer = true;
        playerJumps = false;
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log(rb.velocity);
        if ((transform.position.x>=(leftBorder.x+1f))&& (transform.position.x <= (rightBorder.x - 1f))) {
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y, transform.position.z);
            }
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y, transform.position.z);
            }
        }
        if (!playerJumps)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !playerJumps)
            {
                rb.velocity = transform.up * 6f;
                //transform.position = new Vector3(transform.position.x, transform.position.y+0.2f, transform.position.z);
                playerJumps = true;
            }
        }
        //left mouse: throw frying pan
        if (Input.GetMouseButtonDown(0)&&panWithPlayer)
        {
            fryingPan.SetActive(true);
            direction = new Vector3(fryingPan.transform.position.x + 2f, fryingPan.transform.position.y + 3f, fryingPan.transform.position.z);
            fryingPan.GetComponent<Rigidbody>().AddForce(direction * direction.magnitude * 20);
            panWithPlayer = false;
        }
        //right mouse: attack with spatula
        if (Input.GetMouseButtonDown(1))
        {
            spatula.SetActive(true);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (playerJumps&&rb.velocity==new Vector3(0,0,0))
        {
            playerJumps = false;
        }
    }

}
