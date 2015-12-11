using UnityEngine;
using System.Collections;
using PathologicalGames;

public class SmallAsteroid : MonoBehaviour {

	public AudioClip asteroidCrashSound;
	public float health = 2f;
	public ParticleSystem explodePrefab;

	private MainCamera shakeCam;

	void Awake ()
	{
		shakeCam = Camera.main.GetComponent<MainCamera>();
	}
	
	void OnBecameInvisible()
	{
		Despawn ();
	}
	
	void DamageObject ()
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
			shakeCam.Shake();
			AudioSource.PlayClipAtPoint(asteroidCrashSound,transform.position);
		}
	}
	
	void Despawn ()
	{
		PoolManager.Pools["Asteroids"].Despawn(this.transform);
	}
	
	void BlowUp () 
	{
		PoolManager.Pools["Particles"].Spawn(explodePrefab, transform.position, Quaternion.Euler(90, 0, Random.Range(0, 360)));
		Despawn ();
	}
}

