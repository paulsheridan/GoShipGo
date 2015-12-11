using UnityEngine;
using System.Collections;

public class MainCamera : MonoBehaviour 
{
	private Vector3 originPosition;
	private Quaternion originRotation = Quaternion.identity;
	private float shake_decay;
	private float shake_intensity;

	void Awake ()
	{
		originPosition = transform.position;
	}

	void Update () 
	{
		if (shake_intensity > 0)
		{
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-shake_intensity,shake_intensity) * .02f,
				originRotation.y + Random.Range (-shake_intensity,shake_intensity) * .02f,
				originRotation.z + Random.Range (-shake_intensity,shake_intensity) * .02f,
				originRotation.w + Random.Range (-shake_intensity,shake_intensity) * .02f);
			shake_intensity -= shake_decay;
			transform.position = originPosition;
		}
	}

	public void Shake ()
	{
		shake_intensity = .7f;
		shake_decay = 0.15f;
	}
}
