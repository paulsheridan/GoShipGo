using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PoliceGun : MonoBehaviour 
{
	public LayerMask policeMask;
	public Rigidbody2D laser;
	public AudioClip laserShot;
	public float distance;

	private float laserSpeed = -16f;
	private float fireRate = 0.65F;
	private float nextFire;
	private bool fire = false;
	
	void Update () 
	{
		RaycastHit2D gunRayHit = Physics2D.Raycast (transform.position, -Vector2.up, 15, policeMask);
		if (gunRayHit == true)
		{
			StartCoroutine(Fire ());
		}

		if (fire == true && Time.time > nextFire)
		{
			nextFire = Time.time + fireRate;
			Transform laserInstance = PoolManager.Pools["Weapons"].Spawn ("Laser", transform.position, Quaternion.Euler (new Vector3 (0,0,0)));
			laserInstance.GetComponent<Rigidbody2D>().AddForce (new Vector2 (Random.Range(-2.2f, 2.2f), laserSpeed));
			AudioSource.PlayClipAtPoint(laserShot,transform.position);
		}
	}

	IEnumerator Fire ()
	{
		float fireTime = (Random.Range(0.2f, 0.6f));
		fire = true;
		yield return new WaitForSeconds(fireTime);
		fire = false;
	}
}
