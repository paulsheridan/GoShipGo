using UnityEngine;
using System.Collections;
using PathologicalGames;

public class SpawnAsteroids : MonoBehaviour 
{
	private float spawnMin = 0.4f;
	private float spawnMax = 0.9f;
	private float spawnMinSmall = 1f;
	private float spawnMaxSmall = 8f;
	private float spawnMinP = 14f;
	private float spawnMaxP = 20f;
	public float downForce;
	private float diagonalForce = 1200;
	private float gameDuration;

	IEnumerator Start () 
	{
		float waitTime = Random.Range (4f, 9f);
		float waitTimePowerUp = Random.Range (4f, 7f);
		yield return new WaitForSeconds(3f);
		SpawnBig ();
		yield return new WaitForSeconds(waitTime);
		SpawnSmallLeft ();
		yield return new WaitForSeconds(waitTime);
		SpawnSmallRight ();
		yield return new WaitForSeconds(waitTimePowerUp);
		SpawnHealth ();
	}

	void Update ()
	{
		gameDuration += Time.deltaTime;
		downForce = 750 + (gameDuration * 4);
	}
	void SpawnBig () 
	{
		string asteroid;
		int randnum = Random.Range (1, 4);
		if (randnum == 1)
			asteroid = "Asteroid";
		else if (randnum == 2)
			asteroid = "Asteroid3";
		else
			asteroid = "Asteroid2";
		Vector3 spawnPosition = new Vector3(Random.Range(-6.5f, 6.5f), 21, 0);
		Transform instance = PoolManager.Pools["Asteroids"].Spawn (asteroid, spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
		instance.GetComponent<Rigidbody2D>().AddForce (Vector3.down * downForce);
		instance.GetComponent<Rigidbody2D>().AddTorque (Random.Range (-60, 60));
		Invoke ("SpawnBig", Random.Range (spawnMin, spawnMax));
	}

	void SpawnSmallLeft () 
	{
		Vector3 spawnPosition = new Vector3(Random.Range(-5.0f, -2.0f), Random.Range(16.0f, 24.0f), 0);
		float smallVector = Random.Range(-1.2f, -2.3f);
		Transform instance = PoolManager.Pools["Asteroids"].Spawn ("AsteroidSmall", spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
		instance.GetComponent<Rigidbody2D>().AddForce (new Vector2(1, smallVector) * diagonalForce);
		Invoke ("SpawnSmallLeft", Random.Range (spawnMinSmall, spawnMaxSmall));
	}

	void SpawnSmallRight () 
	{
		Vector3 spawnPosition = new Vector3(Random.Range(5.0f, 2.0f), Random.Range(16.0f, 24.0f), 0);
		float smallVector = Random.Range(-1.9f, -2.3f);
		Transform instance = PoolManager.Pools["Asteroids"].Spawn ("AsteroidSmall", spawnPosition, Quaternion.Euler(0, 0, Random.Range(0, 360)));
		instance.GetComponent<Rigidbody2D>().AddForce (new Vector2(-1, smallVector) * diagonalForce);
		Invoke ("SpawnSmallRight", Random.Range (spawnMinSmall, spawnMaxSmall));
	}
	void SpawnHealth () 
	{
		Vector3 spawnPosition = new Vector3(Random.Range(-5.5f, 5.5f), 21, 0);
		Transform instance = PoolManager.Pools["Asteroids"].Spawn ("HealthPickUp", spawnPosition, Quaternion.Euler(0, 0, 0));
		instance.GetComponent<Rigidbody2D>().AddForce (Vector3.down * 500);
		Invoke ("SpawnHealth", Random.Range (spawnMinP, spawnMaxP));
	}
}
