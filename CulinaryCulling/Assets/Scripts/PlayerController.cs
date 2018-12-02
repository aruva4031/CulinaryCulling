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
    Transform panPos;

    bool playerJumps;

    public bool rotate;
    public float rotateAmount;
    public float rotateAngle;
    public float duration;

    public bool rotateBack;

    Vector3 direction;

    bool clicked;
    bool doubleclicked;
    float tempTime;
    public float speed;

    public GameObject mousePoint;

    //private enum playerState { IDLE,WALKING, JUMPING, SPATULA_ATTACK, FRYING_PAN_ATTACK };
    //private playerState current_state = playerState.IDLE;
    private Rigidbody rb;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        spatula = transform.GetChild(0).gameObject;
        fryingPan = transform.GetChild(1).transform.GetChild(0).gameObject;
        panPos = fryingPan.transform;
        panWithPlayer = true;
        playerJumps = false;
        rotate = false;
        clicked = false;
        doubleclicked = false;
        speed = 0.1f;
    }
	
	// Update is called once per frame
	void Update () {

        if ((transform.position.x >= (leftBorder.x + 1f)) ) {
            //movement to left
            if (Input.GetKey(KeyCode.A))
            {
                transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
            }
        }
        //if you are within the border constraint of left
        if(transform.position.x <= (rightBorder.x - 1f)) {
            //if you didn't click before, start a timer
            if (Input.GetKeyDown(KeyCode.D)&&!clicked)
            {
                tempTime = Time.time;
                clicked = true;
            }
            //if the distance between the two clicks is close enough, do the double click
            else if (Input.GetKey(KeyCode.D) && ((Time.time - tempTime) <= 1f) && clicked)
            {
                speed = 0.3f;
                doubleclicked = true;
                clicked = false;
            }
            else if (Input.GetKey(KeyCode.D) && ((Time.time - tempTime) > 1f) && clicked)
            {
                clicked = false;
            }

            //movement to right
            if (Input.GetKey(KeyCode.D))
            {
                transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
            }
            else if (Input.GetKeyUp(KeyCode.D)&&doubleclicked)
            {
                speed = 0.1f;
                doubleclicked = false;
                clicked = false;
            }
        }
        else if (Input.GetKeyUp(KeyCode.D) && doubleclicked)
        {
            speed = 0.1f;
            clicked = false;
        }
        else if(transform.transform.position.x > (leftBorder.x + 1f))
        {

        }
        if (!playerJumps)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !playerJumps)
            {
                rb.velocity = transform.up * 6f;
                playerJumps = true;
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("In");
            fryingPan.SetActive(true);
            fryingPan.GetComponent<Rigidbody>().useGravity = false;
            fryingPan.GetComponent<FryingPanScript>().tempTime = Time.time;
        }

        //right mouse: attack with spatula
        if (Input.GetMouseButtonDown(1))
        {
            spatula.SetActive(true);
            rotate = true;
        }

        //spatula rotation
        spatulaRotation();
    }

    void spatulaRotation()
    {
        Vector3 eulerRotation = spatula.transform.rotation.eulerAngles;

        if (rotate && eulerRotation.z > 220f)
        {
            spatula.transform.Rotate(0, 0, -rotateAmount);
        }
        if (rotate && eulerRotation.z <= 220f)
        {
            rotate = false;
            StartCoroutine(backToIdle(duration));
        }

        if (rotateBack && eulerRotation.z < 330f)
        {
            spatula.transform.Rotate(0, 0, rotateAmount);

        }
        if (rotateBack && eulerRotation.z >= 330f)
        {
            rotateBack = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (playerJumps&&rb.velocity==new Vector3(0,0,0))
        {
            playerJumps = false;
        }
    }

    IEnumerator backToIdle(float duration)
    {
        yield return new WaitForSeconds(duration);
        rotateBack = true;
    }
    IEnumerator returnPan(float duration)
    {
        yield return new WaitForSeconds(duration);
        fryingPan.SetActive(false);
        fryingPan.transform.position = panPos.position;
        mousePoint.SetActive(false);
    }

}
