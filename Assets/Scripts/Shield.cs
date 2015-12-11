using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour 
{
	public float shieldTime = 1f;
	
	void Start () 
	{
		GetComponent<Renderer>().enabled = false;
	}
	
	public void ShowShield () 
	{
		//StartCoroutine(ShieldCoroutine());
	}

	IEnumerator ShieldCoroutine ()
	{
		GetComponent<Renderer>().enabled = true;
		yield return new WaitForSeconds (1);
		GetComponent<Renderer>().enabled = false;
	}
}
