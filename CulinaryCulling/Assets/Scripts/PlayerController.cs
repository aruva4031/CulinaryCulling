using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public Vector3 leftBorder;
    public Vector3 rightBorder;
    public Vector3 upperBorder;
    public Vector3 lowerBorder;

    public GameObject spatulaLeft;
    public GameObject spatulaRight;
    public GameObject fryingPanLeft;
    public GameObject fryingPanRight;
    public Camera cam;

    bool panWithPlayer;
    //Transform panPos;

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

    public Vector3 spatulaPos1;
    public Vector3 spatulaPos2;

    //private enum playerState { IDLE,WALKING, JUMPING, SPATULA_ATTACK, FRYING_PAN_ATTACK };
    //private playerState current_state = playerState.IDLE;
    private Rigidbody rb;

    public bool playerFreeze;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        spatulaRight= transform.GetChild(0).gameObject;
        spatulaLeft = transform.GetChild(1).gameObject;
        //fryingPan = transform.GetChild(1).transform.GetChild(0).gameObject;
        //panPos = fryingPan.transform;
        panWithPlayer = true;
        playerJumps = false;
        rotate = false;
        clicked = false;
        doubleclicked = false;
        speed = 0.1f;
        playerFreeze = false;
        //spatulaPos1= new Vector3(spatula.transform.position.x - 21.4f, spatula.transform.position.y + 2.5f, spatula.transform.position.z);
        //spatulaPos2 = spatula.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        if (playerFreeze == false)
        {
            if ((transform.position.x >= (leftBorder.x + 1f)))
            {
                //movement to left
                if (Input.GetKeyDown(KeyCode.A))
                {
                    spatulaLeft.gameObject.SetActive(true);
                    spatulaRight.gameObject.SetActive(false);
                    GetComponent<BefriendEnemy>().isLeft = true;
                    GetComponent<BefriendEnemy>().directionAmount=-2;
                    //spatula.transform.rotation = Quaternion.Euler(spatula.transform.rotation.x, 180, spatula.transform.rotation.z);
                    //spatula.transform.position = spatulaPos1;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    transform.position = new Vector3(transform.position.x - speed, transform.position.y, transform.position.z);
                }
            }
            //if you are within the border constraint of left
            if (transform.position.x <= (rightBorder.x - 1f))
            {
                ////if you didn't click before, start a timer
                //if (Input.GetKeyDown(KeyCode.D) && !clicked)
                //{
                //    tempTime = Time.time;
                //    clicked = true;
                //}
                ////if the distance between the two clicks is close enough, do the double click
                //else if (Input.GetKey(KeyCode.D) && ((Time.time - tempTime) <= 1f) && clicked)
                //{
                //    speed = 0.3f;
                //    doubleclicked = true;
                //    clicked = false;
                //}
                //else if (Input.GetKey(KeyCode.D) && ((Time.time - tempTime) > 1f) && clicked)
                //{
                //    clicked = false;
                //}

                //movement to right
                if (Input.GetKeyDown(KeyCode.D))
                {
                    GetComponent<BefriendEnemy>().isLeft = false;
                    GetComponent<BefriendEnemy>().directionAmount = 2;
                    spatulaRight.gameObject.SetActive(true);
                    spatulaLeft.gameObject.SetActive(false);
                    //spatula.transform.rotation = Quaternion.Euler(spatula.transform.rotation.x, 180, spatula.transform.position.z);
                    //spatula.transform.position = spatulaPos2;
                }
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position = new Vector3(transform.position.x + speed, transform.position.y, transform.position.z);
                }
                //else if (Input.GetKeyUp(KeyCode.D) && doubleclicked)
                //{
                //    speed = 0.1f;
                //    doubleclicked = false;
                //    clicked = false;
                //}
            }
            //else if (Input.GetKeyUp(KeyCode.D) && doubleclicked)
            //{
            //    speed = 0.1f;
            //    clicked = false;
            //}
            //else if (transform.transform.position.x > (leftBorder.x + 1f))
            //{

            //}
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
                //spatulaLeft.SetActive(false);
                //spatulaRight.SetActive(false);
                if (spatulaLeft.activeSelf)
                {
                    spatulaLeft.SetActive(false);
                    fryingPanLeft.SetActive(true);
                    fryingPanLeft.GetComponent<Rigidbody>().useGravity = false;
                    fryingPanLeft.GetComponent<FryingPanScript>().tempTime = Time.time;
                }
                else if (spatulaRight.activeSelf)
                {
                    fryingPanRight.SetActive(true);
                    spatulaRight.SetActive(false);
                    fryingPanRight.GetComponent<Rigidbody>().useGravity = false;
                    fryingPanRight.GetComponent<FryingPanScript>().tempTime = Time.time;
                }
            }

            //right mouse: attack with spatula
            if (Input.GetMouseButtonDown(1))
            {
                //spatula.SetActive(true);
                rotate = true;
            }

            //spatula rotation
            if (spatulaLeft.activeSelf == true)
            {
                spatulaRotation(spatulaLeft);
            }
            else if(spatulaRight.activeSelf == true)
            {
                spatulaRotation(spatulaRight);
            }
        }
    }

    void spatulaRotation(GameObject spatula)
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
            //spatula.SetActive(false);
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
    //IEnumerator returnPan(float duration)
    //{
    //    yield return new WaitForSeconds(duration);
    //    fryingPan.SetActive(false);
    //    fryingPan.transform.position = panPos.position;
    //    mousePoint.SetActive(false);
    //}

}
