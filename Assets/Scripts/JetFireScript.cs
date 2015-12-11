using UnityEngine;
using System.Collections;

public class JetFireScript : MonoBehaviour {

	private float timeMin = 0.05f;
	private float timeMax = 0.1f;


	void Start ()
	{
		ChangePosLeft ();
	}
	void ChangePosLeft () 
	{
		float angle = Random.Range (-25f, 0f);
		transform.localEulerAngles = new Vector3 (0, 0, angle);
		Invoke ("ChangePosRight", Random.Range (timeMin, timeMax));
	}

	void ChangePosRight () 
	{
		float angle = Random.Range (0f, 25f);
		transform.localEulerAngles = new Vector3 (0, 0, angle);
		Invoke ("ChangePosLeft", Random.Range (timeMin, timeMax));
	}
}
