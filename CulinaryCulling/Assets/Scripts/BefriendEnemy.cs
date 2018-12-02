using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BefriendEnemy : MonoBehaviour {
    public GameObject net;
    public List<GameObject> friends = new List<GameObject>();
    public int numberOfFriends;
    public UnityEngine.UI.Text[] numbers = new UnityEngine.UI.Text[4];
    Vector3 netPos;
    bool netWithPlayer;

    public bool chooseToKill;

    Vector3 direction;

    // Use this for initialization
    void Start()
    {
        net.GetComponent<Rigidbody>().useGravity = false;
        //numberOfFriends = 0;
        //chooseToKill = false;
        netPos = net.transform.position;
        netWithPlayer = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)&&netWithPlayer)
        {
            //Debug.Log("E");
            net.SetActive(true);
            direction = new Vector3(net.transform.position.x + 2f, net.transform.position.y + 3f, net.transform.position.z) - net.transform.position;
            net.GetComponent<Rigidbody>().useGravity = true;
            net.GetComponent<Rigidbody>().AddForce(direction * direction.magnitude * 20);
            netWithPlayer = false;

        }
    }

    public void resetNet()
    {
        StartCoroutine(netReset(0f));
    }

    IEnumerator netReset(float duration)
    {
        yield return new WaitForSeconds(duration);
        net.transform.position = netPos;
        net.SetActive(false);
    }

    public void addFriend(GameObject newFriend)
    {
        if (numberOfFriends > 3)
        {
            int num = 4;
            foreach (GameObject friend in friends)
            {
                //display number on the head
                num++;
            }
        }
        else
        {
            friends.Add(newFriend);
        }
    }
}
