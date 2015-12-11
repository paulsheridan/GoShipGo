using UnityEngine;
using System.Collections;

public class Police : MonoBehaviour 
{
	private Vector3 originPos;
	private Vector3 rightEndPos;
	private Vector3 leftEndPos;
	private float leftDistance;
	private float rightDistance;
	private float randomChoice;
	private float dodgeDistance = 5f;
	private float lastHitTime;
	private float repeatDamagePeriod = 0.1f;
	private MainCamera shakeCam;
	private GameObject spawner;

	public GameObject leftRayOrigin;
	public GameObject rightRayOrigin;
	public GameObject explosion;
	//public GameObject smoke;
	public float dodgeTime;
	public float dodgeMultiplier = 1.25f;
	public float health = 3;
	public float hitNumber = 0;
	
	void Awake () 
	{
		shakeCam = Camera.main.GetComponent<MainCamera>();
		spawner = GameObject.Find("Police Spawner");
	}

	void Update () 
	{
		RaycastHit2D leftRay = Physics2D.Raycast (leftRayOrigin.transform.position, Vector2.up);
		leftDistance = Mathf.Abs(leftRay.point.y - transform.position.y);
		RaycastHit2D rightRay = Physics2D.Raycast (rightRayOrigin.transform.position, Vector2.up);
		rightDistance = Mathf.Abs(rightRay.point.y - transform.position.y);
		if (leftDistance < dodgeDistance && rightDistance < dodgeDistance)
		{
			randomChoice = Random.Range(0f, 2f);
			if (randomChoice >= 1f)
				DodgeLeft (20 * dodgeMultiplier);
			else
				DodgeRight (20 * dodgeMultiplier);
		}
		if (leftDistance < dodgeDistance)
		{
			DodgeRight(10 * dodgeMultiplier);
		}
		if (rightDistance < dodgeDistance)
		{
			DodgeLeft(10 * dodgeMultiplier);
		}
	
		if (transform.position.x < -5)
		{
			dodgeTime += Time.deltaTime;
			if (dodgeTime >= 0.4)
			{
				dodgeTime = 0;
				DodgeRight (15 * dodgeMultiplier);
			}
		}
		if (transform.position.x > 5)
		{
			dodgeTime += Time.deltaTime;
			if (dodgeTime >= 0.4)
			{
				dodgeTime = 0;
				DodgeLeft (15 * dodgeMultiplier);
			}
		}
		if (transform.position.x < 0.5 && transform.position.x > -0.5)
			GetComponent<Rigidbody2D>().isKinematic = false;
	}

	void DodgeLeft (float dodgeSpeed)
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2 (-dodgeSpeed, 0);
	}

	void DodgeRight (float dodgeSpeed)
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2 (dodgeSpeed, 0);
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		if(col.gameObject.tag == "Asteroid")
		{
			float hitVector = Random.Range(-800f, 800f);
			col.GetComponent<Rigidbody2D>().AddForce (Vector3.right * hitVector);
			gameObject.GetComponent<Rigidbody2D>().AddForce (Vector3.right * - hitVector);
			gameObject.GetComponent<Rigidbody2D>().AddTorque (Random.Range (-100, 100));
			if (Time.time > lastHitTime + repeatDamagePeriod) 
			{
				DamageObject ();
				shakeCam.Shake();
				hitNumber ++;
			}
		}
	}

	void BlowUp ()
	{
		//Instantiate (explosion, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
		spawner.SendMessage ("Destroyed");
		Destroy (this.gameObject);
	}

	void DamageObject ()
	{
		if(health > 0f)
		{
			health -= 1; 
			lastHitTime = Time.time; 
		}
		else
		{
			BlowUp ();
		}
	}
}
