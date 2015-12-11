using UnityEngine;
using System.Collections;
using PathologicalGames;

public class Asteroid : MonoBehaviour {
	
	public AudioClip asteroidCrashSound;
	public GameObject missileRemains;
	public GameObject explode;
	public ParticleSystem particlePrefab;
	public LayerMask objectsToDestroy;
	public LayerMask raycastLayers = -9;

	private Rigidbody2D vel;
	private MainCamera shakeCam;
	private Vector2 velV3;
	private Vector3 rot;
	private float health = 1;
	private float missilePushRadius = 10;
	private float bombForce = 1400;
	private bool raysOn = true;
	private GameObject player;
	
	void Awake()
	{
		shakeCam = Camera.main.GetComponent<MainCamera>();
		player = GameObject.Find ("Player");
	}

	void Update()
	{
		RaycastHit2D rightRay = Physics2D.Raycast (transform.position, Vector2.right, Mathf.Infinity, raycastLayers);
		RaycastHit2D leftRay = Physics2D.Raycast (transform.position, -Vector2.right, Mathf.Infinity, raycastLayers);
		if (raysOn == true)
			if (rightRay.collider != null || leftRay.collider != null)
			{
				player.SendMessage ("PlusTen");
				raysOn = false;
			}
	}

	void OnBecameInvisible()
	{
		if (this.gameObject.activeInHierarchy)
		{
			Despawn ();
		}
	}

	void DamageObject()
	{
		if (health > 0)
		{
			health -= 1;
		}
		else
		{
			BlowUp ();
		}
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		if(col.gameObject.tag == "Asteroid")
		{
			CamShake ();
			PoolManager.Pools["Particles"].Spawn(particlePrefab, transform.position, Quaternion.Euler(90, 0, Random.Range(0, 360)));
			AudioSource.PlayClipAtPoint(asteroidCrashSound,transform.position);
		}
	}

	void CamShake ()
	{
		shakeCam.Shake();
	}
	
	void Despawn()
	{
		health = 2;
		PoolManager.Pools["Asteroids"].Despawn(this.transform);
		raysOn = true;
	}

	void BlowUp () 
	{
		rot = transform.eulerAngles;
		PoolManager.Pools["Particles"].Spawn(particlePrefab, transform.position, Quaternion.identity);
		Instantiate (missileRemains, transform.position, Quaternion.Euler(rot));
		MissileDetonation ();
		CamShake ();
		Despawn ();
	}

	void MissileDetonation()
	{
		Collider2D [] asteroidsFar = Physics2D.OverlapCircleAll (transform.position, missilePushRadius, objectsToDestroy);
		foreach(Collider2D en in asteroidsFar)
		{
			Rigidbody2D rb = en.GetComponent<Rigidbody2D>();
			if(rb != null && (rb.tag == "Asteroid" || rb.tag == "AsteroidChunk"))
			{
				Vector3 deltaVec = rb.transform.position - transform.position;
				Vector3 expForce = deltaVec.normalized * bombForce;
				rb.AddForce (expForce);
			}
		}
	}
}
