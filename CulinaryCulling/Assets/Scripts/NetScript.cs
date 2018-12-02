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
        GetComponent<Rigidbody>().constraints= RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        if (other.GetComponent<Collider>().gameObject.tag == "Enemy")
        {
            player.GetComponent<BefriendEnemy>().addFriend(other.GetComponent<Collider>().gameObject);
        }
        else
        {
            player.GetComponent<BefriendEnemy>().resetNet();
        }
    }
}
