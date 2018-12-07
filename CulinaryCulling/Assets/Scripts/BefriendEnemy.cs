using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class BefriendEnemy : MonoBehaviour {
    public GameObject net;
    public List<GameObject> friends = new List<GameObject>();
    public int numberOfFriends;
    public UnityEngine.UI.Text[] numbers = new UnityEngine.UI.Text[4];
    Vector3 netPos;
    bool netWithPlayer;

    public bool chooseToKill;
    public int toRemove;
    public GameObject friendPlaceholder;

    public bool isLeft = false;
    public int directionAmount;
    public GameObject leftPos;
    public GameObject rightPos;

    Vector3 direction;

    //cooking
    public List<Recipe> abominations = new List<Recipe>();
    public List<GameObject> combine;
    public bool chooseToCook;

    public float health;
    public UnityEngine.UI.Text healthDisplay;

    // Use this for initialization
    void Start()
    {
        health = 10;
        net.GetComponent<Rigidbody>().useGravity = false;
        numberOfFriends = 0;
        netWithPlayer = true;
        chooseToKill = false;
        chooseToCook = false;
        if (isLeft)
        {
            net.transform.position = new Vector3(leftPos.transform.position.x-1f, leftPos.transform.position.y+2f, leftPos.transform.position.z);
        }
        else
        {
            net.transform.position = new Vector3(rightPos.transform.position.x + 1f, rightPos.transform.position.y+2f, rightPos.transform.position.z);
        }
        netPos = net.transform.position;
        toRemove = -1;
    }

    // Update is called once per frame
    void Update()
    {
        healthDisplay.text = "Health: " + (int)health;
        if (Input.GetKeyDown(KeyCode.E)&&netWithPlayer)
        {
            //Debug.Log("E");
            if (isLeft)
            {
                net.transform.position = new Vector3(leftPos.transform.position.x - 1f, leftPos.transform.position.y+2f, leftPos.transform.position.z);
            }
            else
            {
                net.transform.position = new Vector3(rightPos.transform.position.x + 1f, rightPos.transform.position.y+2f, rightPos.transform.position.z);
            }
            netPos = net.transform.position;
            GetComponent<PlayerController>().spatulaLeft.SetActive(false);
            GetComponent<PlayerController>().spatulaRight.SetActive(false);
            GetComponent<PlayerController>().playerFreeze = true;
            net.SetActive(true);
 
            if (directionAmount < 0)
            {
                direction = new Vector3(net.transform.position.x -2f, net.transform.position.y + 3f, net.transform.position.z) - net.transform.position;
            }
            else
            {
                direction = new Vector3(net.transform.position.x + 2f, net.transform.position.y + 3f, net.transform.position.z) - net.transform.position;
            }
            
            net.GetComponent<Rigidbody>().useGravity = true;

            net.GetComponent<Rigidbody>().AddForce(direction * direction.magnitude * 15);
            netWithPlayer = false;
        }
        if (chooseToKill)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                toRemove = 0;
                removeFriend();
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                toRemove = 1;
                removeFriend();
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                toRemove = 2;
                removeFriend();
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                toRemove = 3;
                removeFriend();
            }
        }
        if (Input.GetKeyDown(KeyCode.C)&&!chooseToCook)
        {
            activateText();
        }
        else if (Input.GetKeyDown(KeyCode.C)&&chooseToCook)
        {
            deactivateText();
            cook();
        }

        else if (Input.GetKeyDown(KeyCode.V) && chooseToCook)
        {
            deactivateText();
            cookHealth();
        }
        if (chooseToCook)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                combine.Add(friends[0]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                combine.Add(friends[1]);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                combine.Add(friends[2]);
            }
        }
        if (chooseToCook)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
        if (health <= 0)
        {
            playerDies();
        }
    }

    void activateText()
    {
        Debug.Log("In C Display");
        int num = 0;
        foreach (GameObject friend in friends)
        {
            Debug.Log("In C Loop1");
            //display number on the head
            numbers[num].gameObject.SetActive(true);
            numbers[num].transform.position = new Vector3(friend.transform.position.x, friend.transform.position.y + 1f, friend.transform.position.z);
            num++;
            Debug.Log("In C Loop2");
        }
        chooseToCook = true;
    }

    void deactivateText()
    {
        foreach (UnityEngine.UI.Text number in numbers)
        {
            number.gameObject.SetActive(false);
        }
    }

    public void resetNet(float duration)
    {
        StartCoroutine(netReset(duration));
    }

    IEnumerator netReset(float duration)
    {
        yield return new WaitForSeconds(duration);
        net.transform.position = netPos;
        net.SetActive(false);
        netWithPlayer = true;
        net.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        GetComponent<PlayerController>().playerFreeze = false;
    }

    void removeFriend()
    {
        foreach (UnityEngine.UI.Text number in numbers)
        {
            number.gameObject.SetActive(false);
        }
        if (toRemove != 3)
        {
            friendPlaceholder.GetComponent<EnemyAI>().beFriended = true;
            friends.Add(friendPlaceholder);
            //set enemy friendly
            friendPlaceholder = null;
            Destroy(friends[toRemove].gameObject);
        }
        else
        {
            Destroy(friendPlaceholder.gameObject);
            friendPlaceholder = null;
        }
        chooseToKill = false;
        resetNet(1f);
    }

    public void addFriend(GameObject newFriend)
    {
        if (friends.Count == 3)
        {
            int num = 0;
            foreach (GameObject friend in friends)
            {
                //display number on the head
                numbers[num].transform.position = new Vector3(friend.transform.position.x, friend.transform.position.y+2f, friend.transform.position.z);
                numbers[num].gameObject.SetActive(true);
                num++;
            }
            numbers[3].transform.position = new Vector3(newFriend.transform.position.x, newFriend.transform.position.y + 2f, newFriend.transform.position.z);
            numbers[3].gameObject.SetActive(true);
            friendPlaceholder = newFriend;
            chooseToKill = true;
        }
        else
        {
            newFriend.GetComponent<EnemyAI>().beFriended = true;
            friends.Add(newFriend);
            //set enemy friendly
            resetNet(2f);
        }
        Debug.Log("Friends: " + friends.Count);
    }

    public void cook()
    {
        //create list of ingredients to be combined
        List<string> ingredients=new List<string>();
        foreach (GameObject ingredient in combine)
        {
            ingredients.Add(ingredient.gameObject.name);
        }
        //loop through recipes
        foreach (Recipe abom in abominations)
        {
            if (abom.hasIngredients(ingredients))
            {
                Vector3 abomPos = combine[0].transform.position;
                Vector3 abomRot = combine[0].transform.rotation.eulerAngles;
                foreach(GameObject ingredient in combine.ToList())
                {
                    Destroy(ingredient);
                }
                //combine.Clear();
                combine = new List<GameObject>();
                Instantiate(abom,abomPos, Quaternion.Euler(abomRot));
                chooseToCook = false;
                return;
            }
        }
        chooseToCook = false;
        //call function in recipe: compare them
        //if the function returns true: destroy all objects in combine, instantiate abomination at position of first combine object
    }

    public void cookHealth()
    {
        int healthLength = 0;
        switch (combine.Count)
        {
            case 1:
                healthLength = 2;
                break;
            case 2:
                healthLength = 3;
                break;
            case 3:
                healthLength = 5;
                break;
        }
        foreach (GameObject ingredient in combine.ToList())
        {
            Destroy(ingredient);
        }
        if (health + healthLength > 10)
        {
            health = 10;
        }
        else
        {
            health += healthLength;
        }
        chooseToCook = false;
    }

    public void playerDies()
    {
        health = 0;
        SceneManager.LoadScene("CookingDemo");
    }

    //private void OnTriggerEnter(Collider collision)
    //{
    //    Debug.Log("IN");
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        health--;
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            health-=0.2f;
        }
    }
}
