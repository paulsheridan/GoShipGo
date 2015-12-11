using UnityEngine;
using System.Collections;

public class DespawnAsteroid : MonoBehaviour 
{
	void OnBecameInvisible()
	{
		Destroy(gameObject);
	}
}
