using UnityEngine;
using System.Collections;
using PathologicalGames;

public class Laser : MonoBehaviour 
{
	//public GameObject hit;

	void OnBecameInvisible ()
	{
		if (this.gameObject.activeInHierarchy)
		{
			Despawn ();
		}
	}

	void OnTriggerEnter2D (Collider2D col)
	{
		col.gameObject.SendMessage ("DamageObject");
		Despawn ();
	}

	void OnCollisionEnter2D (Collision2D col)
	{
		col.gameObject.SendMessage ("DamageObject");
		Despawn ();
	}

	void Despawn ()
	{
		//Instantiate (hit, transform.position, Quaternion.Euler(90, Random.Range(0, 90), 0));
		PoolManager.Pools["Weapons"].Despawn(this.transform);
	}
}
