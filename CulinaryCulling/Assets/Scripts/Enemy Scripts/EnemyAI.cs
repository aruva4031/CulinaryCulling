using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour {

	public int health;

	public float speed;

	public Vector3 strt;
	public Vector3 nd;

	public bool beFriended = false;

	public int followOffset=0;

	public enum OccilationFuntion {Sine, Cosine}

    public float highestTransitionLevel;

	// Transform  TomatoTrans;
	// public bool grounded = true;
	// public float speed = 5;
	// public LayerMask EnemyMask;
	// float TomatoWidth;
	// Rigidbody TomatoRB;
	void Start () {
		// TomatoRB = GetComponent<Rigidbody>();
		// TomatoWidth = this.GetComponent<MeshRenderer>().bounds.extents.x;
		// TomatoTrans = this.transform;
	
	}
	
	// Update is called once per frame
	void Update () {
        if (health <= 0)
        {
            Destroy(this.gameObject);
        }
	}

	void FixedUpdate()
	{
		// Vector3 lineCastPos = TomatoTrans.position - TomatoTrans.right * TomatoWidth;
		// Debug.DrawLine(lineCastPos, lineCastPos + Vector3.down);
		// grounded = Physics.Linecast(lineCastPos, lineCastPos + Vector3.down, EnemyMask);	
		

		// if(!grounded)
		// {
		// 	Vector3 currentRot = TomatoTrans.eulerAngles;
		// 	currentRot.y += 180;
		// 	TomatoTrans.eulerAngles = currentRot;
		// }
		
		
		
		// Vector3 TomatoVel = TomatoRB.velocity;
		// TomatoVel.x = -TomatoTrans.right.x * speed;
		// TomatoRB.velocity = TomatoVel;
	}

	public void moveOnPlatform(GameObject enemy, LayerMask EnemyMask)
	{
		Transform enemyTrans = enemy.transform;
		Rigidbody enemyRB = enemy.GetComponent<Rigidbody>();

		float enemyWidth = enemy.GetComponent<MeshRenderer>().bounds.extents.x;
		bool grounded;

		Vector3 lineCastPos = enemyTrans.position - enemyTrans.right * enemyWidth;
		Debug.DrawLine(lineCastPos, lineCastPos + Vector3.down);
		grounded = Physics.Linecast(lineCastPos, lineCastPos + Vector3.down, EnemyMask);	
		

		if(!grounded)
		{
			Vector3 currentRot = enemyTrans.eulerAngles;
			currentRot.y += 180;
			enemyTrans.eulerAngles = currentRot;
		}
		
		Vector3 enemyVel = enemyRB.velocity;
		enemyVel.x = -enemyTrans.right.x * speed;
		enemyRB.velocity = enemyVel;

	}

     public IEnumerator Oscillate (OccilationFuntion method, float scalar)
     {
         while (true)
         {
             if (method == OccilationFuntion.Sine)
             {
                 transform.position = new Vector3 (transform.position.x + Mathf.Sin (Time.time) * scalar, transform.position.y, transform.position.z );
             }
             else if (method == OccilationFuntion.Cosine)
             {
                 transform.position = new Vector3(transform.position.x + Mathf.Cos(Time.time) * scalar, transform.position.y, transform.position.z );
             }
             yield return new WaitForEndOfFrame ();
         }
     }
	public void move(GameObject enemy, Vector3 start,Vector3 end, float movementSpeed, bool face)
	{
		Transform enemyTrans = enemy.transform;
		Rigidbody enemyRB = enemy.GetComponent<Rigidbody>();
		Vector3 enemyVel = enemyRB.velocity;
		float eDistance = Vector3.Distance(enemyTrans.position, end);
		enemyTrans.position = new Vector3(PingPong(Time.time* movementSpeed, start.x, end.x), enemyTrans.position.y, enemyTrans.position.z);
	}

	public float PingPong(float t, float minLen, float maxLen)
	{
		return Mathf.PingPong(t, maxLen-minLen) + minLen;
	}
	public void Flip(bool facing)
	{
		facing = !facing;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;

	}
}
