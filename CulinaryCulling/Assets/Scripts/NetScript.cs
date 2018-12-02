using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetScript : MonoBehaviour {

    public GameObject player;
    // Use this for initialization
    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("In Trigger");
        //StartCoroutine(freezeConstraints());
        if (other.GetComponent<Collider>().gameObject.tag == "Enemy"&& !player.GetComponent<BefriendEnemy>().friends.Contains((other.GetComponent<Collider>().gameObject)))
        {
            Debug.Log("In Enemy");
            player.GetComponent<BefriendEnemy>().addFriend(other.GetComponent<Collider>().gameObject);
            //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
            //player.GetComponent<PlayerController>().playerFreeze=true;
            //player.GetComponent<BefriendEnemy>().resetNet(2f);
            StartCoroutine(freezeConstraints());
        }
        else
        {
            player.GetComponent<BefriendEnemy>().resetNet(0f);
        }
    }

    IEnumerator freezeConstraints()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }
}
