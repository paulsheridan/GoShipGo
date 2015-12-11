using UnityEngine;
using System.Collections;
using PathologicalGames;

public class GunScript : MonoBehaviour
{
	public AudioClip laserShot;

	private float laserSpeed = 13f;
	private float fireRate = 0.15f;
	private float nextFire = 0.0f;
	private float charge = 100f;
	private float laserRechargePeriod = 3;
	private float lastShotTime;
	private SpriteRenderer coolDownBar;
	private Vector3 coolDownScale;
	
	void Awake () 
	{
		coolDownBar = GameObject.Find("AmmoBar").GetComponent<SpriteRenderer>();
		coolDownScale = coolDownBar.transform.localScale;
	}

	void Update ()
	{
		if (Input.GetButton("Fire3"))
		{
			FireLaser();
		}

		if (charge < 100 && Time.time > lastShotTime + laserRechargePeriod)
		{
			charge += 0.1f;
			UpdateCoolDownBar ();
		}
	}

	public void FireLaser()
	{
		if (charge > 0 && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Transform laserInstance = PoolManager.Pools["Weapons"].Spawn ("Laser", transform.position, Quaternion.Euler (new Vector3 (0,0,0)));
			laserInstance.GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range(-.9f, .9f), laserSpeed));
			AudioSource.PlayClipAtPoint(laserShot,transform.position);
			charge -= 7f;
			lastShotTime = Time.time;
			UpdateCoolDownBar ();
		}
	}
	
	public void UpdateCoolDownBar ()
	{
		coolDownBar.transform.localScale = new Vector3(coolDownScale.x * charge * 0.01f, 1f, 1f);
	}
}
