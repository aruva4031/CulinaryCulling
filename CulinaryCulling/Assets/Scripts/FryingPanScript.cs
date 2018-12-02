using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FryingPanScript : MonoBehaviour {

    bool panWithPlayer;
    bool coroutine_running;
    public GameObject mousePoint;
    public float returnAmount;

    public GameObject panPos;
    Vector3 panRot;

    Vector3 direction;
    public float tempTime;

    bool panGone;

    public float timer;
    public float directionAmount;

    // Use this for initialization
    void Start () {
        coroutine_running=false;
        panWithPlayer = true;
        panRot = transform.rotation.eulerAngles;
        panGone = false;
        timer = -1;
        transform.position = new Vector3(panPos.transform.position.x, panPos.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update () {
        //if (Input.GetMouseButtonDown(0)&&panWithPlayer&&!panGone)
        //{
        //    //gameObject.SetActive(true);
        //    GetComponent<Rigidbody>().useGravity = false;
        //    tempTime = Time.time;
        //}
        if (panWithPlayer)
        {
            transform.position = new Vector3(panPos.transform.position.x, panPos.transform.position.y, transform.position.z);
        }
        if (Input.GetMouseButtonUp(0) && ((Time.time - tempTime) < 0.2f)&&panWithPlayer)
        {
            panWithPlayer = false;
            tempTime = Time.time;
            timer = Time.deltaTime;
            mousePoint.transform.position = Input.mousePosition;
            direction = new Vector3(transform.position.x + directionAmount, transform.position.y + 3f, transform.position.z) - transform.position;
            //if (GameObject.Find("Player").transform.rotation.eulerAngles.y <= 90|| GameObject.Find("Player").transform.rotation.eulerAngles.y >= 270)
            //{
            //    direction = new Vector3(transform.position.x + 2f, transform.position.y + 3f, transform.position.z) - transform.position;
            //}
            //else if (GameObject.Find("Player").transform.rotation.eulerAngles.y > 90 && GameObject.Find("Player").transform.rotation.eulerAngles.y < 270)
            //{
            //    direction = new Vector3(transform.position.x - 2f, transform.position.y + 3f, transform.position.z) - transform.position;
            //}
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Rigidbody>().AddForce(direction * direction.magnitude * 15);
            //StartCoroutine(waitForPan());
        }
        else if (Input.GetMouseButton(0)&&panWithPlayer&& ((Time.time - tempTime) >= 0.1f))
        {
            mousePoint.SetActive(true);
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = mousePoint.transform.position.z;
            mousePoint.transform.position = Vector3.MoveTowards(transform.position, target, 100f);
        }
        if (Input.GetMouseButtonUp(0) && (Time.time - tempTime) >= 0.5f&&panWithPlayer)
        {
            panWithPlayer = false;
            Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = mousePoint.transform.position.z;
            mousePoint.transform.position = Vector3.MoveTowards(transform.position, target, 100f);
            Debug.Log("Angle: "+GameObject.Find("Player").transform.rotation.eulerAngles.y);
            if ((directionAmount>0)&&(mousePoint.transform.position.x > transform.position.x))
            {
                GetComponent<Rigidbody>().useGravity = true;
                direction = new Vector3(transform.position.x + 2f + (mousePoint.transform.position.x * 0.2f), transform.position.y + 3f + (mousePoint.transform.position.y * 0.2f), transform.position.z) - transform.position;
                GetComponent<Rigidbody>().AddForce(direction * direction.magnitude * 15);
                //StartCoroutine(waitForPan());
                mousePoint.SetActive(false);
            }
            else if (  (directionAmount<0) && (mousePoint.transform.position.x < transform.position.x))
            {
                GetComponent<Rigidbody>().useGravity = true;
                direction = new Vector3(transform.position.x - 2f + (mousePoint.transform.position.x * 0.2f), transform.position.y + 3f + (mousePoint.transform.position.y * 0.2f), transform.position.z) - transform.position;
                GetComponent<Rigidbody>().AddForce(direction * direction.magnitude * 15);
                //StartCoroutine(waitForPan());
                mousePoint.SetActive(false);
            }
            else
            {
                Debug.Log("NOOO");
                StartCoroutine(returnPan(1f));
            }
        }
        if (coroutine_running)
        {
            timer = -1f;
        }
        //Debug.Log("Secs: "+(Mathf.Abs(Time.deltaTime - timer) % 60));
        //if (((Mathf.Abs(Time.deltaTime - timer))%60) >= 5f&&(timer!=-1f))
        //{
        //    StartCoroutine(returnPan(returnAmount));
        //}
        //if (panGone && !panWithPlayer && !coroutine_running)
        //{
        //    StartCoroutine(returnPan(returnAmount));
        //}
        //else if (panGone && panWithPlayer)
        //{
        //    panGone = false;
        //}
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!panWithPlayer&&!coroutine_running)
        {
            Debug.Log("NOOO2");
            StartCoroutine(returnPan(returnAmount));
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (!panWithPlayer && !coroutine_running)
        {
            Debug.Log("NOOO2");
            StartCoroutine(returnPan(returnAmount));
        }
    }

    //IEnumerator waitForPan()
    //{
    //    yield return new WaitForSeconds(5f);
    //    panGone = true;
    //}

    IEnumerator returnPan(float duration)
    {
        coroutine_running = true;
        yield return new WaitForSeconds(duration);
        transform.position = panPos.transform.position;
        //transform.rotation = Quaternion.Euler(panRot);
        panWithPlayer = true;
        //GetComponent<Rigidbody>().useGravity = true;
        coroutine_running = false;
        panWithPlayer = true;
        panRot = transform.rotation.eulerAngles;
        panGone = false;
        mousePoint.SetActive(false);
        if (directionAmount < 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().spatulaLeft.SetActive(true);
        }
        else if (directionAmount > 0)
        {
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().spatulaRight.SetActive(true);
        }
        coroutine_running = false;
        gameObject.SetActive(false);
    }
}
