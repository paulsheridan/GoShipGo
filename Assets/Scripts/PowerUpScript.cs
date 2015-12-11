using UnityEngine;
using System.Collections;
using PathologicalGames;

public class PowerUpScript : MonoBehaviour {

	void OnBecameInvisible()
	{
		if (this.gameObject.activeInHierarchy)
		{
			Despawn ();
		}
	}

	void OnTriggerEnter2D ()
	{
		Despawn ();
	}

	void Despawn()
	{
		PoolManager.Pools["Asteroids"].Despawn(this.transform);
	}
}
