using UnityEngine;
using System.Collections;

public class PoliceSpawner : MonoBehaviour 
{
	public GameObject police;
	private Vector3 spawnPosition;
	private float rangeBottom = 10f;
	private float rangeTop = 15f;

	void Awake ()
	{
		StartCoroutine(PoliceDespawned ());
		spawnPosition = transform.position;
	}

	IEnumerator PoliceDespawned ()
	{
		yield return new WaitForSeconds(Random.Range (rangeBottom, rangeTop));
		SpawnPolice ();
	}

	void SpawnPolice()
	{
		float position = Random.Range(0f, 4f);
		if (position < 2f)
			spawnPosition = new Vector3(10, 11, 0);
		else
			spawnPosition = new Vector3(-10, 11, 0);
		Instantiate (police, spawnPosition, Quaternion.identity);
			police.GetComponent<Rigidbody2D>().isKinematic = true;
	}

	void Destroyed ()
	{
		StartCoroutine(PoliceDespawned ());
	}
}
